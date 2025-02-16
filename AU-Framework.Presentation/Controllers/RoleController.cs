using AU_Framework.Application.Services;
using AU_Framework.Presentation.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AU_Framework.Presentation.Controllers;

public sealed class RoleController : ApiController
{
    private readonly IRoleService _roleService;

    public RoleController(IMediator mediator, IRoleService roleService) : base(mediator)
    {
        _roleService = roleService;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRole(string userId, string roleName, CancellationToken cancellationToken)
    {
        var result = await _roleService.AssignRoleToUserAsync(userId, roleName, cancellationToken);
        return Ok(result);
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveRole(string userId, string roleName, CancellationToken cancellationToken)
    {
        var result = await _roleService.RemoveRoleFromUserAsync(userId, roleName, cancellationToken);
        return Ok(result);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetUserRoles(string userId, CancellationToken cancellationToken)
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
    [Authorize(Roles = "Admin,Manager,User")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllRolesAsync(cancellationToken);
        return Ok(roles);
    }
} 