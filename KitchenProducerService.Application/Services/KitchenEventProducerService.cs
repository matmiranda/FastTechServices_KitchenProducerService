using KitchenProducerService.Application.DTOs;
using KitchenProducerService.Application.Interfaces;
using KitchenProducerService.Infrastructure.MessageBroker;
using KitchenProducerService.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenProducerService.Application.Services
{
    public class KitchenEventProducerService : IKitchenEventProducerService
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        private readonly IAuthClient _authClient;

        public KitchenEventProducerService(IRabbitMQProducer rabbitMqProducer, IAuthClient authClient)
        {
            _rabbitMqProducer = rabbitMqProducer;
            _authClient = authClient;
        }

        public async Task PublishKitchenEventAsync(KitchenEventRequest request, string token)
        {
            var isValid = await _authClient.ValidateTokenAsync(token);
            if (!isValid)
                throw new UnauthorizedAccessException("Invalid token");

            await _rabbitMqProducer.PublishAsync("KitchenEventQueue", request);
        }
    }
}
