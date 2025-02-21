using AU_Framework.Application.Repository;
using AU_Framework.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Application.Features.UserFeatures.Queries.GetUserCount;

public sealed class GetUserCountQueryHandler : IRequestHandler<GetUserCountQuery, int>
{
    private readonly IRepository<User> _userRepository;

    public GetUserCountQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> Handle(GetUserCountQuery request, CancellationToken cancellationToken)
    {
        var query = await _userRepository.GetAllAsync(cancellationToken);
        return await query.Where(u => !u.IsDeleted).CountAsync(cancellationToken);
    }
} 