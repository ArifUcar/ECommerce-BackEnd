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
[Authorize] // Tüm endpoint'ler için yetkilendirme gerekli
public sealed class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")] // Sadece Admin ve Manager ürün ekleyebilir
    public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = "Admin,Manager")] // Sadece Admin ve Manager ürün güncelleyebilir
    public async Task<IActionResult> Update(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // Sadece Admin ürün silebilir
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return Ok(response);
    }
} 