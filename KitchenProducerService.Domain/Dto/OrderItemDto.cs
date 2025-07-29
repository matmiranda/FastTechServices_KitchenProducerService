namespace KitchenProducerService.Application.Dto
{
    public class OrderItemDto
    {
        public long ItemId { get; set; }
        public long MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
        public decimal TotalItem { get; set; }
        public DateTime ItemCreated { get; set; }
    }
}
