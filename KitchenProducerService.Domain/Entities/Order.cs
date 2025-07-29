
namespace KitchenProducerService.Domain.Entities
{
    public class Order
    {
        public ulong OrderId { get; set; }
        public ulong CustomerId { get; set; }
        public decimal Total { get; set; }
        public string DeliveryMethod { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
        public string? CancelReason { get; set; }
    }
}