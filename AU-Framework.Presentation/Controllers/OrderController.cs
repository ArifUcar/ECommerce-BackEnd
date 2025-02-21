using AU_Framework.Application.Features.OrderFeatures.Commands.CancelOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.DeleteOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrder;
using AU_Framework.Application.Features.OrderFeatures.Queries.GetAllOrders;
using AU_Framework.Application.Features.OrderFeatures.Queries.GetUserOrders;
using AU_Framework.Application.Features.OrderFeatures.Queries.GetOrderCount;
using AU_Framework.Application.Features.OrderFeatures.Queries.GetTotalRevenue;
using AU_Framework.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AU_Framework.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        MessageResponse response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> GetUserOrders(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserOrdersQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        MessageResponse response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        MessageResponse response = await _mediator.Send(new DeleteOrderCommand(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost("cancel/{id}")]
    public async Task<IActionResult> Cancel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        MessageResponse response = await _mediator.Send(new CancelOrderCommand(id), cancellationToken);
        return Ok(response);
    }

    [HttpGet("count")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetOrderCount(CancellationToken cancellationToken)
    {
        var count = await _mediator.Send(new GetOrderCountQuery(), cancellationToken);
        return Ok(new { TotalOrders = count });
    }

    [HttpGet("total-revenue")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetTotalRevenue(CancellationToken cancellationToken)
    {
        var revenue = await _mediator.Send(new GetTotalRevenueQuery(), cancellationToken);
        return Ok(new { TotalRevenue = revenue });
    }
} 