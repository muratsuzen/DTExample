using AutoMapper;
using Business.Abstracts;
using Business.Models;
using Dal.Abstracts;
using Dtos;
using Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;
        IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public void Add(ProductDto dto)
        {
            var productMapper = mapper.Map<Product>(dto);
            productRepository.Add(productMapper);
        }

        public ProductListModel GetList(int index = 0, int size = 10, string searchValue = "", string sortColumn = "")
        {
            var products = productRepository.GetList(
                sortColumn:sortColumn,
                index: index,
                size: size,
                filter: !string.IsNullOrEmpty(searchValue) ? x => x.Code.ToLower().Contains(searchValue.ToLower()) || x.Name.ToLower().Contains(searchValue.ToLower()) : null
                );

            var productsMapper = mapper.Map<ProductListModel>(products);
            return productsMapper;
        }

        public void Update(ProductDto dto)
        {
            var productMapper = mapper.Map<Product>(dto);
            productRepository.Update(productMapper);
        }
    }
}
