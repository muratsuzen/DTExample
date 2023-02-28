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
        public async Task<ProductModel> GetProducts(int pageNumber,int pageSize,string searchValue,string sortColumn)
        {
            string url = $"api/Product/GetList";
            var client = httpClientFactory.CreateClient(name: "WebAPI");

            var modeljson =
                new StringContent(JsonSerializer.Serialize(new 
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    SearchText = searchValue,
                    SortColumn = sortColumn
                }), Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage httpResponseMessage = await client.PostAsync(url,modeljson);

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
