using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AU_Framework.Application.Features.ProductFeatures.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, MessageResponse>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<MessageResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _productService.CreateAsync(request, cancellationToken);
            return new("Ürün başarıyla eklendi");
        }
    }
}
