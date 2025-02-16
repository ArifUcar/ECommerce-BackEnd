using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.CategoryFeatures.Command.DeleteCategory
{
    public sealed record DeleteCategoryCommand(
        string Id
    ) : IRequest<MessageResponse>;
} 