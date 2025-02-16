using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
using AU_Framework.Application.Features.ProductFeatures.CreateProduct;
using AU_Framework.Domain.Entities;
using AutoMapper;

namespace AU_Framework.Persistance.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateProductCommand'dan Product'a dönüşüm yapılacak.
        // ReverseMap() ile ters dönüşüm de sağlanacak.
        CreateMap<CreateProductCommand, Product>().ReverseMap();
        CreateMap<CreateCategoryCommand, Category>().ReverseMap();

    }
}

