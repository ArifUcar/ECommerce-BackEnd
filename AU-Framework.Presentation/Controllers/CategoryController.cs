using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
using AU_Framework.Application.Features.CategoryFeatures.Queries;
using AU_Framework.Domain.Dtos;
using AU_Framework.Domain.Entities;
using AU_Framework.Presentation.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AU_Framework.Presentation.Controllers
{
    // ApiController sınıfından türeyen CategoriesController sınıfı, API isteklerini yöneten controller'dır.
    public sealed class CategoriesController : ApiController
    {
        // Mediator aracılığıyla istekleri işleyecek constructor.
        public CategoriesController(IMediator mediator) : base(mediator) { }

        // HTTP POST isteği ile kategori oluşturma işlemi.
        // [action] ile route kısmındaki kategori oluşturma endpoint'i belirleniyor.
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Mediator aracılığıyla CreateCategoryCommand gönderiliyor.
            // MediatR, uygun handler'ı bulacak ve işlemi gerçekleştirecek.
            MessageResponse response = await _mediator.Send(request, cancellationToken);

            
            return Ok(response);
        }

        [HttpGet("[action]")]  // POST yerine GET kullanıyoruz
        public async Task<IActionResult> GetAllCategory(CancellationToken cancellationToken)  // query parametresini kaldırdık
        {
            GetAllCategoryQuery query = new();  // Yeni bir query instance'ı oluşturuyoruz
            IList<Category> response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }
    }
}
