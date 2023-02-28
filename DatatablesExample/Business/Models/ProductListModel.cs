using Core.Paging;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ProductListModel : BasePageableModel
    {
        public IList<ProductDto> Items { get; set; }
    }
}
