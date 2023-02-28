using Business.Abstracts;
using Core.Paging;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto product)
        {
            for (int i = 0; i < 200; i++)
            {
                var nowTime = DateTime.Now.Ticks.ToString();
                productService.Add(new ProductDto() { Code = $"Product-{nowTime}", Name = $"Product-{nowTime}", Id = Guid.NewGuid() });
            }
            
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDto product)
        {
            productService.Update(product);
            return Ok(product);
        }

        [HttpPost]
        [Route("GetList")]
        public async Task<IActionResult> GetList([FromBody] PageParameters pageParameters)
        {
            var products = productService.GetList(index: pageParameters.PageNumber, size: pageParameters.PageSize, searchValue: pageParameters.SearchText, pageParameters.SortColumn, pageParameters.SortDirection);
            return Ok(products);
        }
    }
}
