using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IProductService
    {
        List<ProductDto> GetList(int index = 0, int size = 10);
        void Add(ProductDto dto);
        void Update(ProductDto dto);
    }
}
