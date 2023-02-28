using Dal.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Abstracts
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
