

using AU_Framework.Application.Features.CategoryFeatures.CreateCategory;

namespace AU_Framework.Application.Services;

    public interface ICategoryService
    {
      Task CreateAsync(CreateCategoryCommand request, CancellationToken cancellationToken);
}

