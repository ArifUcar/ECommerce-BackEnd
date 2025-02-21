using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.UserFeatures.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<List<UserDto>>; 