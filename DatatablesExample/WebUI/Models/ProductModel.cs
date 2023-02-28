using Core.Paging;
using Dtos;

namespace WebUI.Models
{
    public class ProductModel : BasePageableModel
    {
        public IList<ProductDto> Items { get; set; }
    }
}
