using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
using AU_Framework.Application.Features.CategoryFeatures.Command.DeleteCategory;
using AU_Framework.Application.Features.CategoryFeatures.Command.UpdateCategory;
using AU_Framework.Application.Features.CategoryFeatures.Queries;
using AU_Framework.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AU_Framework.Application.Services;

public interface ICategoryService
    {
      Task CreateAsync(CreateCategoryCommand request, CancellationToken cancellationToken);
      Task<IList<Category>> GetAllAsync(GetAllCategoryQuery request, CancellationToken cancellationToken);
      Task UpdateAsync(UpdateCategoryCommand request, CancellationToken cancellationToken);
      Task DeleteAsync(DeleteCategoryCommand request, CancellationToken cancellationToken);
      Task<Category> GetByIdAsync(string id, CancellationToken cancellationToken);
}

