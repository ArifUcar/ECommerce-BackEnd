using AU_Framework.Domain.Entities;
using MediatR;

namespace AU_Framework.Application.Features.CategoryFeatures.Queries
{
    public sealed record GetCategoryByIdQuery(
        Guid Id
    ) : IRequest<Category>;
} 