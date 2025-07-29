using System.ComponentModel.DataAnnotations;

namespace KitchenProducerService.Application.Request
{
    public class KitchenOrderFinalize
    {
        [Required(ErrorMessage = "OrderId é obrigatório.")]
        [Range(1, ulong.MaxValue, ErrorMessage = "OrderId deve ser maior que zero.")]
        public ulong OrderId { get; set; }
    }
}
