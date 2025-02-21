using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.CancelOrder;

public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, MessageResponse>
{
    private readonly IOrderService _orderService;

    public CancelOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<MessageResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        await _orderService.CancelOrderAsync(request.OrderId, cancellationToken);
        return new MessageResponse("Sipariş başarıyla iptal edildi.");
    }
} 