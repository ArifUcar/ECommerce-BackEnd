using AU_Framework.Application.Services;
using AU_Framework.Presentation.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AU_Framework.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ILogger<RoleController> _logger;

    public RoleController(IRoleService roleService, ILogger<RoleController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRole(Guid userId, string roleName, CancellationToken cancellationToken)
    {
        var result = await _roleService.AssignRoleToUserAsync(userId, roleName, cancellationToken);
        return Ok(result);
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveRole(Guid userId, string roleName, CancellationToken cancellationToken)
    {
        var result = await _roleService.RemoveRoleFromUserAsync(userId, roleName, cancellationToken);
        return Ok(result);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetUserRoles(Guid userId, CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetUserRolesAsync(userId, cancellationToken);
        return Ok(roles);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetUsersInRole(string roleName, CancellationToken cancellationToken)
    {
        var users = await _roleService.GetUsersInRoleAsync(roleName, cancellationToken);
        return Ok(users);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _roleService.GetAllRolesAsync(cancellationToken);
            return Ok(roles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Roller listelenirken hata oluştu");
            return BadRequest(new { message = ex.Message });
        }
    }
} 