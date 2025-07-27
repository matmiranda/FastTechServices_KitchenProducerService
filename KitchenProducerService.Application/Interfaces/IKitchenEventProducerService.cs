using KitchenProducerService.Application.DTOs;

namespace KitchenProducerService.Application.Interfaces
{
    public interface IKitchenEventProducerService
    {
        Task PublishKitchenEventAsync(KitchenEventRequest request, string token);
    }
}
