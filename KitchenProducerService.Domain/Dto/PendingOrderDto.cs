namespace KitchenProducerService.Application.Dto
{
    public class PendingOrderDto
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public string DeliveryMethod { get; set; }
        public string Status { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderCreated { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
