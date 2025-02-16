using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, MessageResponse>
{
    private readonly IProductService _productService;

    public UpdateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<MessageResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _productService.UpdateAsync(request, cancellationToken);
        return new MessageResponse("Ürün başarıyla güncellendi.");
    }
} 