using AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.DeleteProduct;
using AU_Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AU_Framework.Domain.Dtos;

namespace AU_Framework.Application.Services
{
    public interface IProductService
    {
        Task CreateAsync(CreateProductCommand request, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateProductCommand request, CancellationToken cancellationToken);
        Task DeleteAsync(DeleteProductCommand request, CancellationToken cancellationToken);
        Task<ProductDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IList<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<IList<ProductDto>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
        Task<ProductStockSummaryDto> GetStockSummaryAsync(CancellationToken cancellationToken);
    }
}
