using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.CancelOrder;

public sealed record CancelOrderCommand(Guid OrderId) : IRequest<MessageResponse>; 