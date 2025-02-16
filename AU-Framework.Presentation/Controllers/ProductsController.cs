using AU_Framework.Application.Features.ProductFeatures.CreateProduct;
using AU_Framework.Domain.Dtos;
using AU_Framework.Presentation.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AU_Framework.Presentation.Controllers;

    public sealed class ProductsController : ApiController
    {
        public ProductsController(IMediator mediator) : base(mediator) {}

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct(CreateProductCommand request,CancellationToken cancellationToken)
        {
           MessageResponse response= await _mediator.Send(request,cancellationToken);
            return Ok(response);
        }
    }

