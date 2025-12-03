using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Tickets
{
    public class Payment
    {
        [Key]
        public Guid Payment_ID { get; set; } = Guid.NewGuid();

        [ForeignKey("BookingID")]
        public Guid BookingID { get; set; }
        public virtual Booking Booking { get; set; }

        [Required]
        [StringLength(100)]
        public string PaymobTransactionID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; } // "Pending", "Success", "Failed", "Refunded"

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } // "Card", "Wallet", 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        [StringLength(500)]
        public string? PaymobOrderID { get; set; }

        [StringLength(1000)]
        public string? ErrorMessage { get; set; }
    }
}
