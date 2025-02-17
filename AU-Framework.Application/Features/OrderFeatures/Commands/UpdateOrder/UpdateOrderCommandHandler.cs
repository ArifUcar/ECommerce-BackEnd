using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrder;

public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, MessageResponse>
{
    private readonly IOrderService _orderService;

    public UpdateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<MessageResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        await _orderService.UpdateAsync(request, cancellationToken);
        return new MessageResponse("Sipariş güncellendi oluşturuldu.");
    }
}

