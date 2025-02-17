using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.CategoryFeatures.Command.UpdateCategory
{
    public sealed record UpdateCategoryCommand(
        Guid Id,
        string CategoryName
    ) : IRequest<MessageResponse>;
} 