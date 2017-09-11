using AutoMapper;
using DataAccess.Entities;
using LogicLayer.DTO;

namespace InternetShop.Tests
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
