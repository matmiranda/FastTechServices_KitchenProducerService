using KitchenProducerService.Application.Dto;

namespace KitchenProducerService.Infrastructure.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<PendingOrderDto>> GetPendingOrdersAsync();
        Task<bool> OrderExistsAsync(ulong orderId);
    }
}
