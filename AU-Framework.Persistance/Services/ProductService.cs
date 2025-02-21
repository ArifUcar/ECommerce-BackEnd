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
    private readonly IImageService _imageService;

    public ProductService(
        IRepository<Product> productRepository,
        IRepository<Category> categoryRepository,
        IMapper mapper,
        ILogger<ProductService> logger,
        IImageService imageService)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
        _imageService = imageService;
    }

    public async Task CreateAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try 
        {
            // Kategori kontrolü
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
            if (category == null)
                throw new Exception("Kategori bulunamadı!");

            if (category.IsDeleted)
                throw new Exception("Bu kategori artık aktif değil!");

            Product product = _mapper.Map<Product>(request);

            if (!string.IsNullOrEmpty(request.Base64Image))
            {
                try 
                {
                    product.Base64Image = request.Base64Image;
                    product.ImagePath = _imageService.SaveBase64Image(request.Base64Image, request.ProductName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Görsel kaydedilirken hata oluştu");
                    throw new Exception("Görsel kaydedilirken bir hata oluştu. Lütfen geçerli bir görsel yükleyin.");
                }
            }

            // ProductDetail'i otomatik olarak oluşturulacak
            await _productRepository.AddAsync(product, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ürün oluşturulurken hata oluştu");
            throw;
        }
    }

    public async Task UpdateAsync(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new Exception("Ürün bulunamadı!");

        if (product.IsDeleted)
            throw new Exception("Bu ürün artık aktif değil!");

        // Kategori kontrolü
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
            throw new Exception("Kategori bulunamadı!");

        if (category.IsDeleted)
            throw new Exception("Bu kategori artık aktif değil!");

        // Ürünü güncelle
        product.ProductName = request.ProductName;
        product.Description = request.Description;
        product.Price = request.Price;
        product.DiscountedPrice = request.DiscountedPrice;
        product.DiscountRate = request.DiscountRate;
        product.DiscountStartDate = request.DiscountStartDate;
        product.DiscountEndDate = request.DiscountEndDate;
        product.StockQuantity = request.StockQuantity;
        product.CategoryId = request.CategoryId;
        product.UpdatedDate = DateTime.Now;

        if (!string.IsNullOrEmpty(request.Base64Image))
        {
            if (!string.IsNullOrEmpty(product.ImagePath))
                _imageService.DeleteImage(product.ImagePath);

            product.Base64Image = request.Base64Image;
            product.ImagePath = _imageService.SaveBase64Image(request.Base64Image, request.ProductName);
        }

        await _productRepository.UpdateAsync(product, cancellationToken);
    }

    public async Task DeleteAsync(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new Exception("Ürün bulunamadı!");

        if (!string.IsNullOrEmpty(product.ImagePath))
            _imageService.DeleteImage(product.ImagePath);

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
                query => query.Include(p => p.Category)
                             .Include(p => p.ProductDetail),
                cancellationToken);

            if (product is null)
                throw new Exception("Ürün bulunamadı!");

            var productDto = _mapper.Map<ProductDto>(product);

            // Eğer görsel varsa ve dosya mevcutsa, base64'e çevir
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                try
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), product.ImagePath);
                    if (File.Exists(fullPath))
                    {
                        var imageBytes = await File.ReadAllBytesAsync(fullPath, cancellationToken);
                        productDto = productDto with { Base64Image = Convert.ToBase64String(imageBytes) };
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Görsel okunurken hata oluştu: {product.ImagePath}");
                }
            }

            return productDto;
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
                             .Include(p => p.ProductDetail)
                             .Where(p => !p.IsDeleted),
                cancellationToken);

            var products = query.ToList();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            // Görselleri base64'e çevir
            var updatedDtos = new List<ProductDto>();
            foreach (var dto in productDtos)
            {
                if (!string.IsNullOrEmpty(dto.ImagePath))
                {
                    try
                    {
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), dto.ImagePath);
                        if (File.Exists(fullPath))
                        {
                            var imageBytes = await File.ReadAllBytesAsync(fullPath, cancellationToken);
                            var base64String = Convert.ToBase64String(imageBytes);
                            updatedDtos.Add(dto with { Base64Image = base64String });
                        }
                        else
                        {
                            updatedDtos.Add(dto);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Görsel okunurken hata oluştu: {dto.ImagePath}");
                        updatedDtos.Add(dto);
                    }
                }
                else
                {
                    updatedDtos.Add(dto);
                }
            }

            return updatedDtos;
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
                             .Include(p => p.ProductDetail)
                             .Where(p => p.CategoryId == categoryId && !p.IsDeleted),
                cancellationToken);

            var products = query.ToList();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            // Görselleri base64'e çevir
            var updatedDtos = new List<ProductDto>();
            foreach (var dto in productDtos)
            {
                if (!string.IsNullOrEmpty(dto.ImagePath))
                {
                    try
                    {
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), dto.ImagePath);
                        if (File.Exists(fullPath))
                        {
                            var imageBytes = await File.ReadAllBytesAsync(fullPath, cancellationToken);
                            var base64String = Convert.ToBase64String(imageBytes);
                            updatedDtos.Add(dto with { Base64Image = base64String });
                        }
                        else
                        {
                            updatedDtos.Add(dto);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Görsel okunurken hata oluştu: {dto.ImagePath}");
                        updatedDtos.Add(dto);
                    }
                }
                else
                {
                    updatedDtos.Add(dto);
                }
            }

            return updatedDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting products by category id: {categoryId}");
            throw;
        }
    }
}
