using MediatR;

namespace AU_Framework.Application.Features.UserFeatures.Queries.GetUserCount;

public sealed record GetUserCountQuery : IRequest<int>; 