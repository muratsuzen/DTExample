using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        IHttpClientFactory httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Datatables()
        {
            return View();
        }
        public async Task<ProductModel> GetProducts(int pageNumber,int pageSize,string searchValue,string sortColumn,bool sortDirection)
        {
            
            var client = httpClientFactory.CreateClient(name: "WebAPI");
            string url = $"{client.BaseAddress}api/Product";

            var modeljson =
                new StringContent(JsonSerializer.Serialize(new
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    SearchText = searchValue,
                    SortColumn = sortColumn,
                    SortDirection = sortDirection
                }), Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpRequestMessage request = new HttpRequestMessage()
            {
                Content = modeljson,
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };
            HttpResponseMessage httpResponseMessage = await client.SendAsync(request);

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ProductModel? models = await httpResponseMessage.Content.ReadFromJsonAsync<ProductModel>();
                return models;
            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> GetData()
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int draw = int.Parse(Request.Form["draw"].FirstOrDefault());
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            var data = await GetProducts(
                pageSize: pageSize, 
                pageNumber: skip / pageSize, 
                searchValue: searchValue, 
                sortDirection: sortColumnDirection=="asc",
                sortColumn: sortColumn);            

            totalRecord = data.Count;
            filterRecord = data.Count;
            
            var returnObj = new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = data.Items
            };

            return Json(returnObj);
        }
    }
}
