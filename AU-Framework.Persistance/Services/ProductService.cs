using AU_Framework.Application.Features.ProductFeatures.CreateProduct;
using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Persistance.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Persistance.Services;

public sealed class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {

        Product product = _mapper.Map<Product>(request);

        await _productRepository.AddAsync(product, cancellationToken);

    }
}
