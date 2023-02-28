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
            for (int i = 0; i < 2000; i++)
            {
                productService.Add(product);
            }
            
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDto product)
        {
            productService.Update(product);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParameters pageParameters)
        {
            var products = productService.GetList(index: pageParameters.PageNumber, size: pageParameters.PageSize);
            return Ok(products);
        }
    }
}
