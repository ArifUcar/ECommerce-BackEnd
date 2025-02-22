using AU_Framework.Domain.Dtos;
using MediatR;
namespace AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory
{

    public sealed record CreateCategoryCommand(
        string CategoryName         
    ) : IRequest<MessageResponse>;  
}
