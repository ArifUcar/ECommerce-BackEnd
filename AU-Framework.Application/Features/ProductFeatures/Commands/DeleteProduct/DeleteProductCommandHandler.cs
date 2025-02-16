using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.DeleteProduct;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, MessageResponse>
{
    private readonly IProductService _productService;

    public DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<MessageResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(request, cancellationToken);
        return new MessageResponse("Ürün başarıyla silindi.");
    }
} 