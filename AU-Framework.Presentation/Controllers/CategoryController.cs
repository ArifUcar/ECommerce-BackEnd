using AU_Framework.Application.Features.CategoryFeatures.CreateCategory; 
using AU_Framework.Domain.Dtos;  
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
        [HttpGet]
        public IActionResult Calculate()
        {
            int x = 0;
            int y = 0;
            int result = x / y;
            return Ok();
        }
    }
}
