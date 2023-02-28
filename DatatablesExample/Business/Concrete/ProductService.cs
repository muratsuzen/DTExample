using AutoMapper;
using Business.Abstracts;
using Dal.Abstracts;
using Dtos;
using Entities;
using System.Linq.Expressions;

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

        public List<ProductDto> GetList(int index = 0, int size = 10)
        {
            var products = productRepository.GetList(index: index, size: size);
            var productsMapper = mapper.Map<List<ProductDto>>(products);
            return productsMapper;
        }

        public void Update(ProductDto dto)
        {
            var productMapper = mapper.Map<Product>(dto);
            productRepository.Update(productMapper);
        }
    }
}
