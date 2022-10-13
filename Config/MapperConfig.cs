using AutoMapper;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Categories;
using CoffeeShopAPI.Models.Orders;
using CoffeeShopAPI.Models.Products;
using CoffeeShopAPI.Models.Users;

namespace CoffeeShopAPI.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Product Mapping
            CreateMap<Product, ProductDetailsDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            // Category Mapping
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            // Order Mapping

            // Users
            CreateMap<Employee, RegisterUserDto>().ReverseMap();
            CreateMap<Employee, LoginUserDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
