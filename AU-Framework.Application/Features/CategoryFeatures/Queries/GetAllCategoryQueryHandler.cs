

using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using MediatR;

namespace AU_Framework.Application.Features.CategoryFeatures.Queries;

public sealed class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IList<Category>>
{
    private readonly ICategoryService _categoryService;

    public GetAllCategoryQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IList<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetAllAsync(request, cancellationToken);
    }
}
    

