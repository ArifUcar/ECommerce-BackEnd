using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AU_Framework.Application.Features.UserFeatures.Queries.GetUserCount;

namespace AU_Framework.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]  // Sadece adminler görebilsin
    public sealed class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetUserCount(CancellationToken cancellationToken)
        {
            var count = await _mediator.Send(new GetUserCountQuery(), cancellationToken);
            return Ok(new { TotalUsers = count });
        }

        // ... diğer endpointler
    }
} 