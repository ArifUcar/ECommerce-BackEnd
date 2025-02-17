using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
using AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrder;
using AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;
using AU_Framework.Domain.Dtos;
using AU_Framework.Domain.Entities;
using AutoMapper;

namespace AU_Framework.Persistance.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null))
            .ForMember(dest => dest.CategoryId,
                opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.StockQuantity,
                opt => opt.MapFrom(src => src.StockQuantity));

        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();

        // Category mappings
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<Category, CategoryDto>();

        // Order mappings
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.UserFullName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.OrderStatus,
                opt => opt.MapFrom(src => src.OrderStatus.Name));

        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.ProductName));
        CreateMap<UpdateOrderCommand, Order>().ReverseMap();

    }
}

