using AutoMapper;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Categories;
using CoffeeShopAPI.Models.Orders;
using CoffeeShopAPI.Models.Products;

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
            CreateMap<Order, OrderDetailsDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
        }
    }
}
