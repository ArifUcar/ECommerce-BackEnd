using AU_Framework.Domain.Dtos;   
using MediatR;                   
namespace AU_Framework.Application.Features.CategoryFeatures.CreateCategory
{
    
    public sealed record CreateCategoryCommand(
        string CategoryName         // Kategori adı, komut ile birlikte gönderilecek tek parametre.
    ) : IRequest<MessageResponse>;  // IRequest<MessageResponse>, bu komutun bir yanıt döndüreceğini ve bu yanıtın MessageResponse tipinde olacağını belirtir.
}
