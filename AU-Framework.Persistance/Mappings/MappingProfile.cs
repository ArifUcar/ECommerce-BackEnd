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
            .ConstructUsing((src, ctx) => new ProductDto(
                Id: src.Id,
                ProductName: src.ProductName,
                Description: src.Description,
                Price: src.Price,
                StockQuantity: src.StockQuantity,
                CategoryId: src.CategoryId,
                CategoryName: src.Category?.CategoryName ?? string.Empty,
                ImagePath: src.ImagePath,
                Base64Image: src.Base64Image,
                CreatedDate: src.CreatedDate
            ));

        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.ImagePath, 
                opt => opt.Ignore())
            .ForMember(dest => dest.Base64Image, 
                opt => opt.MapFrom(src => src.Base64Image));

        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.ImagePath, 
                opt => opt.Ignore())
            .ForMember(dest => dest.Base64Image, 
                opt => opt.MapFrom(src => src.Base64Image));

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

