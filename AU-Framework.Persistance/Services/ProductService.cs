using AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.DeleteProduct;
using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Persistance.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AU_Framework.Domain.Dtos;

namespace AU_Framework.Persistance.Services;

public sealed class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IRepository<Product> productRepository,
        IRepository<Category> categoryRepository,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Kategori kontrolü
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
            throw new Exception("Kategori bulunamadı!");

        if (category.IsDeleted)
            throw new Exception("Bu kategori artık aktif değil!");

        Product product = _mapper.Map<Product>(request);
        await _productRepository.AddAsync(product, cancellationToken);
    }

    public async Task UpdateAsync(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new Exception("Ürün bulunamadı!");

        if (product.IsDeleted)
            throw new Exception("Bu ürün artık aktif değil!");

        // Kategori kontrolü - CategoryId artık Guid
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
            throw new Exception("Kategori bulunamadı!");

        if (category.IsDeleted)
            throw new Exception("Bu kategori artık aktif değil!");

        // Ürünü güncelle
        product.ProductName = request.ProductName;
        product.Description = request.Description;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;
        product.CategoryId = request.CategoryId;  // Artık parse etmeye gerek yok
        product.UpdatedDate = DateTime.Now;

        await _productRepository.UpdateAsync(product, cancellationToken);
    }

    public async Task DeleteAsync(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new Exception("Ürün bulunamadı!");

        // Soft delete
        product.IsDeleted = true;
        product.DeleteDate = DateTime.Now;
        await _productRepository.UpdateAsync(product, cancellationToken);
    }

    public async Task<ProductDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetFirstWithIncludeAsync(
                x => x.Id == id && !x.IsDeleted,
                query => query.Include(p => p.Category),
                cancellationToken);

            if (product is null)
                throw new Exception("Ürün bulunamadı!");

            return _mapper.Map<ProductDto>(product);
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, $"Error getting product by id: {id}");
            throw;
        }
    }

    public async Task<IList<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var query = await _productRepository.GetAllWithIncludeAsync(
                query => query.Include(p => p.Category)
                             .Where(p => !p.IsDeleted),
                cancellationToken);

            var products = query.ToList();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return productDtos;
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Error getting all products");
            throw;
        }
    }

    public async Task<IList<ProductDto>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var query = await _productRepository.GetAllWithIncludeAsync(
                query => query.Include(p => p.Category)
                             .Where(p => p.CategoryId == categoryId && !p.IsDeleted),
                cancellationToken);

            var products = query.ToList();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return productDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting products by category id: {categoryId}");
            throw;
        }
    }
}
