using AU_Framework.Application.Repository;
using AU_Framework.Domain.Dtos;
using AU_Framework.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Application.Features.UserFeatures.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IRepository<User> _userRepository;

    public GetAllUsersQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = await _userRepository.GetAllWithIncludeAsync(
            include => include
                .Include(u => u.Roles)
                .Where(u => !u.IsDeleted),
            cancellationToken);

        return await query.Select(user => new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Phone,
            user.CreatedDate,
            user.LastLoginDate,
            user.IsActive,
            user.Roles.Select(r => r.Name).ToList()
        )).ToListAsync(cancellationToken);
    }
} 