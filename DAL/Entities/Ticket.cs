using DAL.EntityConfigrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [EntityTypeConfiguration(typeof(TicketConfigration))]

    public class Ticket
    {
        [Key]
        public int Ticket_ID { get; set; }
        public string TicketReference { get; set; }
        public int BookingID { get; set; }
        public int TripID { get; set; }
        public int SeatID { get; set; }
        public int ClassID { get; set; }
        public virtual Booking Booking { get; set; } // Ticket belongs to One Booking
        public virtual Trip Trip { get; set; }
        public virtual Seat Seat { get; set; }       // One-to-One: Ticket reserves One Seat
        public virtual Class Class { get; set; }
    }
}
