namespace KitchenProducerService.Domain.Entities
{
    public class OrderItem
    {
        public ulong MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
    }
}
