﻿using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
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
            .AfterMap((src, dest) => {
                if (dest.ProductDetail == null)
                {
                    dest.ProductDetail = new ProductDetail();
                }
                
                dest.ProductDetail.Color = src.Color;
                dest.ProductDetail.Size = src.Size;
                dest.ProductDetail.Material = src.Material;
                dest.ProductDetail.Brand = src.Brand;
                dest.ProductDetail.Model = src.Model;
                dest.ProductDetail.Warranty = src.Warranty;
                dest.ProductDetail.Specifications = src.Specifications;
                dest.ProductDetail.AdditionalInformation = src.AdditionalInformation;
                dest.ProductDetail.Weight = src.Weight;
                dest.ProductDetail.WeightUnit = src.WeightUnit;
                dest.ProductDetail.Dimensions = src.Dimensions;
                dest.ProductDetail.StockCode = src.StockCode;
                dest.ProductDetail.Barcode = src.Barcode;
            });

        CreateMap<ProductDetailDto, ProductDetail>();

        // Category mappings
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<Category, CategoryDto>();

        // Order mappings
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.UserFullName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.OrderStatus,
                opt => opt.MapFrom(src => src.OrderStatus.Name))
            .ForMember(dest => dest.OrderDetails,
                opt => opt.MapFrom(src => src.OrderDetails));

        // OrderDetail mappings
        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : string.Empty))
            .ForMember(dest => dest.ProductId,
                opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity,
                opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.UnitPrice));

        CreateMap<UpdateOrderCommand, Order>().ReverseMap();
    }
}

