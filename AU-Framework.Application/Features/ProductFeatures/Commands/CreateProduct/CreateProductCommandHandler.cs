using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;

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
        return new MessageResponse("Ürün başarıyla oluşturuldu.");
    }
} 