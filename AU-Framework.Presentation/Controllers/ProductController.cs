using AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.DeleteProduct;
using AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;
using AU_Framework.Application.Features.ProductFeatures.Queries.GetAllProducts;
using AU_Framework.Application.Features.ProductFeatures.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AU_Framework.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("[action]/{id}")]
    [AllowAnonymous] // Bu endpoint için yetkilendirme gerekmez
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("[action]")]
    [Authorize(Roles = "Admin,Manager")] // Sadece Admin ve Manager erişebilir
    public async Task<IActionResult> Update(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("[action]/{id}")]
    [Authorize(Roles = "Admin")] // Sadece Admin erişebilir
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return Ok(response);
    }
} 