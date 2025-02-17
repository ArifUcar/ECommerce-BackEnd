using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;

using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, MessageResponse>
{
    private readonly IOrderService _orderService;

    public CreateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<MessageResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await _orderService.CreateAsync(request, cancellationToken);
        return new MessageResponse("Sipariş başarıyla oluşturuldu.");
    }
} 