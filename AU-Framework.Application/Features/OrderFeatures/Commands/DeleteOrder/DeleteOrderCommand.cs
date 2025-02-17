using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.DeleteOrder;

    public sealed record DeleteOrderCommand(
        Guid Id):IRequest<MessageResponse>;
    
    

