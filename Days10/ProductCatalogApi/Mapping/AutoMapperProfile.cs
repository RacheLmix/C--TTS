using AutoMapper;
using ProductCatalogApi.Data;
using ProductCatalogApi.DTOs;

namespace ProductCatalogApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
} 