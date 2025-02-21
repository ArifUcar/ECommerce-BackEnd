using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrderStatus;

public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, MessageResponse>
{
    private readonly IOrderService _orderService;

    public UpdateOrderStatusCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<MessageResponse> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        await _orderService.UpdateOrderStatusAsync(request, cancellationToken);
        return new MessageResponse("Sipariş durumu başarıyla güncellendi.");
    }
} 