using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
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
}

