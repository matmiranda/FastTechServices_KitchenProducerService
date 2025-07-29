using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KitchenProducerService.Application.Interfaces;
using KitchenProducerService.Application.Request;

namespace MenuProducerService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KitchenProducerController : ControllerBase
    {
        private readonly IKitchenProducerService _service;

        public KitchenProducerController(IKitchenProducerService service)
        {
            _service = service;
        }

        //Visualizar pedidos recebidos (pendentes)
        [HttpGet("pendentes")]
        [Authorize(Roles = "COZINHEIRO")]
        public async Task<IActionResult> ListarPedidosPendentes() =>
            await _service.GetPendingOrdersAsync();

        //Aceitar pedido
        [HttpPut("aceitar")]
        [Authorize(Roles = "COZINHEIRO")]
        public async Task<IActionResult> AceitarPedido([FromBody] KitchenOrderAccept request) =>
            await _service.PublishOrderAcceptAsync(request);

        //Rejeitar pedido com justificativa
        [HttpPut("rejeitar")]
        [Authorize(Roles = "COZINHEIRO")]
        public async Task<IActionResult> RejeitarPedido([FromBody] KitchenOrderReject request) =>
            await _service.PublishOrderRejectAsync(request);

        //finaliza pedido
        [HttpPut("pronto")]
        [Authorize(Roles = "COZINHEIRO,ATENDENTE")]
        public async Task<IActionResult> FinalizarPedido([FromBody] KitchenOrderReady request) =>
            await _service.PublishOrderReadyAsync(request);

    }
}
