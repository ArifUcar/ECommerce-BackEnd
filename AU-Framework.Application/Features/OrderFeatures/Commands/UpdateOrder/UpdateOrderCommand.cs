﻿using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrder;

    public sealed record UpdateOrderCommand(
        Guid Id,
            Guid UserId,
    DateTime OrderDate,
    Guid OrderStatusId,
    decimal TotalAmount,
    List<OrderDetailDto> OrderDetails

        ):IRequest<MessageResponse>;
    
    

