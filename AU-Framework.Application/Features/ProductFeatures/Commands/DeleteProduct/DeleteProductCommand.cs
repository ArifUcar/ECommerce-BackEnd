using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
    Guid Id
) : IRequest<MessageResponse>; 