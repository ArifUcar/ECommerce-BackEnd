using AU_Framework.Domain.Entities;

namespace AU_Framework.Application.Services;

public interface IRoleService
{
    Task<bool> AssignRoleToUserAsync(string userId, string roleName, CancellationToken cancellationToken);
    Task<bool> RemoveRoleFromUserAsync(string userId, string roleName, CancellationToken cancellationToken);
    Task<IList<Role>> GetUserRolesAsync(string userId, CancellationToken cancellationToken);
    Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken);
    Task<bool> IsUserInRoleAsync(string userId, string roleName, CancellationToken cancellationToken);
    Task<IList<Role>> GetAllRolesAsync(CancellationToken cancellationToken);
} 