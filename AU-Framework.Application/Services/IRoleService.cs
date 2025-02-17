using AU_Framework.Domain.Entities;

namespace AU_Framework.Application.Services;

public interface IRoleService
{
    Task<bool> AssignRoleToUserAsync(Guid userId, string roleName, CancellationToken cancellationToken);
    Task<bool> RemoveRoleFromUserAsync(Guid userId, string roleName, CancellationToken cancellationToken);
    Task<IList<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken);
    Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken);
    Task<bool> IsUserInRoleAsync(Guid userId, string roleName, CancellationToken cancellationToken);
    Task<IList<Role>> GetAllRolesAsync(CancellationToken cancellationToken);
} 