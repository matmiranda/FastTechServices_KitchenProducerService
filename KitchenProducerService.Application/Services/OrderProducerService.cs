using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KitchenProducerService.Application.Interfaces;
using KitchenProducerService.Application.Request;
using KitchenProducerService.Application.Response;
using KitchenProducerService.Infrastructure.MessageBroker;
using KitchenProducerService.Infrastructure.Security;
using KitchenProducerService.Infrastructure.Repository;

namespace KitchenProducerService.Application.Services
{
    public class KitchenProducerService : IKitchenProducerService
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        private readonly IAuthClient _authClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly string MessageSent = "Item enviado com sucesso para a fila.";

        public KitchenProducerService(
            IRabbitMQProducer rabbitMqProducer,
            IAuthClient authClient,
            IHttpContextAccessor httpContextAccessor,
            IOrderRepository orderRepository)
        {
            _rabbitMqProducer = rabbitMqProducer;
            _authClient = authClient;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> PublishOrderAcceptAsync(KitchenOrderAccept request)
        {
            await ValidateTokenAsync();

            var exists = await _orderRepository.OrderExistsAsync(request.OrderId);
            if (!exists)
            {
                return new NotFoundObjectResult(new OrderResponse
                {
                    Message = $"Pedido com ID {request.OrderId} não encontrado."
                });
            }

            await _rabbitMqProducer.PublishAsync(QueueNames.OrderAccepted, request);

            return new OkObjectResult(new OrderResponse { Message = MessageSent });
        }

        public async Task<IActionResult> PublishOrderRejectAsync(KitchenOrderReject request)
        {
            await ValidateTokenAsync();

            var exists = await _orderRepository.OrderExistsAsync(request.OrderId);
            if (!exists)
            {
                return new NotFoundObjectResult(new OrderResponse
                {
                    Message = $"Pedido com ID {request.OrderId} não encontrado."
                });
            }

            await _rabbitMqProducer.PublishAsync(QueueNames.OrderRejected, request);

            return new OkObjectResult(new OrderResponse { Message = MessageSent });
        }

        public async Task<IActionResult> GetPendingOrdersAsync()
        {
            await ValidateTokenAsync();

            var orders = await _orderRepository.GetPendingOrdersAsync();

            return new OkObjectResult(orders);
        }

        public async Task<IActionResult> PublishOrderReadyAsync(KitchenOrderReady request)
        {
            await ValidateTokenAsync();

            var exists = await _orderRepository.OrderExistsAsync(request.OrderId);
            if (!exists)
            {
                return new NotFoundObjectResult(new OrderResponse
                {
                    Message = $"Pedido com ID {request.OrderId} não encontrado."
                });
            }

            await _rabbitMqProducer.PublishAsync(QueueNames.OrderReady, request);

            return new OkObjectResult(new OrderResponse { Message = MessageSent });
        }

        public async Task ValidateTokenAsync()
        {
            var headers = _httpContextAccessor.HttpContext?.Request?.Headers;

            if (headers == null || !headers.TryGetValue("Authorization", out var token))
                throw new UnauthorizedAccessException("Token não encontrado no header.");

            if (!token.ToString().StartsWith("Bearer "))
                throw new UnauthorizedAccessException("Formato inválido do token.");

            var cleanToken = token.ToString().Replace("Bearer ", "");
            var isValid = await _authClient.ValidateTokenAsync(cleanToken);

            if (!isValid)
                throw new UnauthorizedAccessException("Token inválido.");
        }
    }
}
