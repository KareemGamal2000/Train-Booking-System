using DAL.Entities.Trips;
using DAL.EntityConfigrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Tickets
{
    [EntityTypeConfiguration(typeof(TicketConfigration))]

    public class Ticket
    {
        [Key]
        public Guid Ticket_ID { get; set; } = Guid.NewGuid();
        public string TicketReference { get; set; }
        public Guid BookingID { get; set; }
        public Guid TripID { get; set; }
        public Guid SeatID { get; set; }
        public Guid ClassID { get; set; }
        public virtual Booking Booking { get; set; } // Ticket belongs to One Booking
        public virtual Trip Trip { get; set; }
        public virtual Seat Seat { get; set; }       // One-to-One: Ticket reserves One Seat
        public virtual Class Class { get; set; }
    }
}
