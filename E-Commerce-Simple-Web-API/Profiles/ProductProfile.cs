using AutoMapper

using E_Commerce_Simple_Web_API.DTOs;
using E_Commerce_Simple_Web_API.Models;

namespace E_Commerce_Simple_Web_API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Product Entity to DTOs
            CreateMap<Product, ProductListDto>();
            CreateMap<Product, ProductDetailDto>();

            // DTOs to Product Entity
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
