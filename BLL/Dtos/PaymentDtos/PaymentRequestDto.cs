using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.PaymentDtos
{
    public class PaymentRequestDto
    {
        [Required]
        public Guid BookingID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; } = "All"; // Default value
    }
}