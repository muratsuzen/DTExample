using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var model = await GetProducts();
            return View(model);
        }
        async Task<List<ProductModel>> GetProducts()
        {
            string url = $"api/product";
            var client = httpClientFactory.CreateClient(name: "WebAPI");
            HttpRequestMessage httpRequestMessage = new(method: HttpMethod.Get, requestUri: url);
            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<ProductModel>? models = await httpResponseMessage.Content.ReadFromJsonAsync<List<ProductModel>>();
                return models;
            }
            return null;
        }
    }
}
