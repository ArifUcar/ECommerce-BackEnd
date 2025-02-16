using AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.DeleteProduct;
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
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IRepository<Product> productRepository,
        IRepository<Category> categoryRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Kategori kontrolü
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId.ToString(), cancellationToken);
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
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId.ToString(), cancellationToken);
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

    public async Task<Product> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product == null || product.IsDeleted)
            throw new Exception("Ürün bulunamadı!");

        return product;
    }

    public async Task<IList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _productRepository.FindAsync(
            x => !x.IsDeleted,
            cancellationToken);

        return products.ToList();
    }
}
