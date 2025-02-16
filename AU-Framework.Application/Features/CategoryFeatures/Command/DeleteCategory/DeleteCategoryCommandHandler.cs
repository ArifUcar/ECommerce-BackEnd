using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.CategoryFeatures.Command.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, MessageResponse>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<MessageResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(request, cancellationToken);
            return new MessageResponse("Kategori başarıyla silindi.");
        }
    }
} 