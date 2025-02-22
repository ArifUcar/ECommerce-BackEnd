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
                src.Id,
                src.ProductName,
                src.Description,
                src.Price,
                src.DiscountedPrice,
                src.DiscountRate,
                src.DiscountStartDate,
                src.DiscountEndDate,
                src.IsDiscounted,
                src.StockQuantity,
                src.CategoryId,
                src.Category?.CategoryName ?? string.Empty,
                src.ImagePath,
                src.Base64Image,
                src.CreatedDate,
                src.ProductDetail != null ? ctx.Mapper.Map<ProductDetailDto>(src.ProductDetail) : null
            ));

        CreateMap<ProductDetail, ProductDetailDto>()
            .ConstructUsing(src => new ProductDetailDto(
                src.Id,
                src.Color,
                src.Size,
                src.Material,
                src.Brand,
                src.Model,
                src.Warranty,
                src.Specifications,
                src.AdditionalInformation,
                src.Weight,
                src.WeightUnit,
                src.Dimensions,
                src.StockCode,
                src.Barcode
            ));

        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .ForMember(dest => dest.Base64Image, opt => opt.MapFrom(src => src.Base64Image))
            .ForMember(dest => dest.DiscountedPrice, opt => opt.MapFrom(src => src.DiscountedPrice))
            .ForMember(dest => dest.DiscountRate, opt => opt.MapFrom(src => src.DiscountRate))
            .ForMember(dest => dest.DiscountStartDate, opt => opt.MapFrom(src => src.DiscountStartDate))
            .ForMember(dest => dest.DiscountEndDate, opt => opt.MapFrom(src => src.DiscountEndDate))
            .ForMember(dest => dest.ProductDetail, opt => opt.MapFrom(src => src.ProductDetail));

        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .ForMember(dest => dest.Base64Image, opt => opt.MapFrom(src => src.Base64Image))
            .ForMember(dest => dest.ProductDetail, opt => opt.Ignore());

        CreateMap<ProductDetailDto, ProductDetail>();

        // Category mappings
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<Category, CategoryDto>();

        // Order mappings
        CreateMap<CreateOrderCommand, Order>();
        

        // OrderDetail mappings
        CreateMap<OrderDetail, OrderDetailResponse>()
            .ConstructUsing(src => new OrderDetailResponse(
                src.Id,
                src.ProductId,
                src.ProductName,
                src.Quantity,
                src.UnitPrice,
                src.SubTotal,
                src.CreatedDate,
                src.UpdatedDate,
                src.DeleteDate
            
            ));

        CreateMap<UpdateOrderCommand, Order>().ReverseMap();
    }
}

