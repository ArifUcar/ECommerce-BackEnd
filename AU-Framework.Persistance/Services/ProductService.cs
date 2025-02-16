using AU_Framework.Application.Features.ProductFeatures.CreateProduct;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Persistance.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Persistance.Services;

public sealed class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {

        Product product = _mapper.Map<Product>(request);

   
        await _context.Set<Product>().AddAsync(product,cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
