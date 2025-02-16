using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.CategoryFeatures.Command.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, MessageResponse>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<MessageResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateAsync(request, cancellationToken);
            return new MessageResponse("Kategori başarıyla güncellendi.");
        }
    }
} 