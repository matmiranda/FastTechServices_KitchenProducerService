using KitchenProducerService.Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace KitchenProducerService.Application.Interfaces
{
    public interface IKitchenProducerService
    {
        Task<IActionResult> PublishOrderAcceptAsync(KitchenOrderAccept request);
        Task<IActionResult> PublishOrderRejectAsync(KitchenOrderReject request);
        Task<IActionResult> PublishOrderReadyAsync(KitchenOrderReady request);
        Task<IActionResult> GetPendingOrdersAsync();
        Task ValidateTokenAsync();
    }
}
