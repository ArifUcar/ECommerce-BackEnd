using AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;
using AU_Framework.Application.Features.OrderFeatures.Queries.GetAllOrders;
using AU_Framework.Presentation.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AU_Framework.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrderController : ApiController
{
    public OrderController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);
        return Ok(response);
    }

    // Diğer CRUD endpoint'leri...
} 