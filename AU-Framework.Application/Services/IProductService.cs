using AU_Framework.Application.Features.ProductFeatures.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AU_Framework.Application.Services
{
    public interface IProductService
    {
        Task CreateAsync(CreateProductCommand request,CancellationToken cancellationToken);
    }
}
