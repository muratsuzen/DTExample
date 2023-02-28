using Business.Models;
using Core.Paging;
using Dtos;
using Entities;
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
        ProductListModel GetList(int index = 0, int size = 10,string searchValue = "",string sortColumn = "");
        void Add(ProductDto dto);
        void Update(ProductDto dto);
    }
}
