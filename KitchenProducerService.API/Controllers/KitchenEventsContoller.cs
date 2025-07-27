using KitchenProducerService.Application.DTOs;
using KitchenProducerService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitchenProducerService.API.Controllers
{
    [Route("api/[controller]")]
    public class KitchenEventsContoller : ControllerBase
    {
        private readonly IKitchenEventProducerService _kitchenEventProducerService;

        public KitchenEventsContoller(IKitchenEventProducerService KitchenEventProducerService)
        {
            _kitchenEventProducerService = KitchenEventProducerService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] KitchenEventRequest request)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _kitchenEventProducerService.PublishKitchenEventAsync(request, token);
            return Ok(new { message = "Pedido enviado com sucesso para a fila." });
        }
    }

}
