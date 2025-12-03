using Data.EntityConfigrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Tickets
{
    [EntityTypeConfiguration(typeof(TicketConfigration))]

    public class Ticket
    {
        [Key]
        public Guid Ticket_ID { get; set; } = Guid.NewGuid();
        public string? TicketReference { get; set; }
        public Guid Booking_ID { get; set; }
        public virtual Booking Booking { get; set; } // Ticket belongs to One Booking
        public int SeatID { get; set; }
        public virtual Seat Seat { get; set; }       // Ticket reserves One Seat
        public long ClassID { get; set; }
        public virtual Class Class { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

    }
}
