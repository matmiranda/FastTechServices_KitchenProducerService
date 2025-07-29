using System.ComponentModel.DataAnnotations;

namespace KitchenProducerService.Application.Request
{
    public class KitchenOrderReject
    {
        [Required(ErrorMessage = "OrderId é obrigatório.")]
        [Range(1, ulong.MaxValue, ErrorMessage = "OrderId deve ser maior que zero.")]
        public ulong OrderId { get; set; }

        [Required(ErrorMessage = "Cancel Reason é obrigatória.")]
        [MinLength(5, ErrorMessage = "Cancel Reason deve ter pelo menos 5 caracteres.")]
        [MaxLength(250, ErrorMessage = "Cancel Reason não pode exceder 250 caracteres.")]
        public required string CancelReason { get; set; }
    }
}
