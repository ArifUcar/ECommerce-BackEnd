using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Persistance.Services;

public sealed class RoleService : IRoleService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;

    public RoleService(
        IRepository<User> userRepository,
        IRepository<Role> roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<bool> AssignRoleToUserAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        var role = await _roleRepository.GetFirstAsync(r => r.Name == roleName, cancellationToken);
        if (role is null)
            throw new Exception("Rol bulunamadı!");

        if (user.Roles.Any(r => r.Name == roleName))
            return true; // Rol zaten atanmış

        user.Roles.Add(role);
        await _userRepository.UpdateAsync(user, cancellationToken);
        return true;
    }

    public async Task<bool> RemoveRoleFromUserAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        var role = user.Roles.FirstOrDefault(r => r.Name == roleName);
        if (role is null)
            return true; // Rol zaten yok

        user.Roles.Remove(role);
        await _userRepository.UpdateAsync(user, cancellationToken);
        return true;
    }

    public async Task<IList<Role>> GetUserRolesAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        return user.Roles.ToList();
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetFirstAsync(r => r.Name == roleName, cancellationToken);
        if (role is null)
            throw new Exception("Rol bulunamadı!");

        return role.Users.ToList();
    }

    public async Task<bool> IsUserInRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        return user.Roles.Any(r => r.Name == roleName);
    }

    public async Task<IList<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);
        return await roles.ToListAsync(cancellationToken);
    }
} 