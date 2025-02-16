using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using MediatR;

namespace AU_Framework.Application.Features.CategoryFeatures.Queries
{
    public sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryByIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
} 