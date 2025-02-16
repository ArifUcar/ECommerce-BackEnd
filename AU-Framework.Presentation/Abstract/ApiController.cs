using MediatR;  
using Microsoft.AspNetCore.Mvc;  

namespace AU_Framework.Presentation.Abstract
{
  
    [ApiController]

    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        // IMediator, MediatR kütüphanesinin işleyicisi (handler) aracılığıyla mesajları yönlendiren bir araçtır.
        public readonly IMediator _mediator;

        // ApiController yapıcı (constructor) metodu, IMediator bağımlılığını alır.
        public ApiController(IMediator mediator)
        {
            _mediator = mediator;  // Mediator, uygulamanın isteklerini yönlendirecek ve işleme yapacaktır.
        }
    }
}
