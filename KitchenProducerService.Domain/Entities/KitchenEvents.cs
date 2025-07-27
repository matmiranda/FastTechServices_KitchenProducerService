namespace KitchenProducerService.Domain.Entities
{
    public class KitchenEvents
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? Justification { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
