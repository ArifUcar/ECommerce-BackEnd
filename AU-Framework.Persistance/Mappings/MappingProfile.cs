using AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;
using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
using AU_Framework.Domain.Entities;
using AutoMapper;
using AU_Framework.Domain.Dtos;

namespace AU_Framework.Persistance.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateProductCommand'dan Product'a dönüşüm yapılacak.
        // ReverseMap() ile ters dönüşüm de sağlanacak.
        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.CategoryId, 
                      opt => opt.MapFrom(src => src.CategoryId));

        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.CategoryId, 
                      opt => opt.MapFrom(src => src.CategoryId));

        CreateMap<CreateCategoryCommand, Category>().ReverseMap();
        // Order mappings
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.UserFullName, 
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.OrderStatus, 
                opt => opt.MapFrom(src => src.OrderStatus.Name));

        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.ProductName));
    }
}

