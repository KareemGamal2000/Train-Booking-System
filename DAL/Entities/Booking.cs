using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Trips;
using DAL.Entities.Tickets;

namespace DAL.Entities
{
    public class Booking
    {
        [Key]
        public Guid Booking_ID { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; } // "Confirmed", "Cancelled"
        public decimal TotalPrice { get; set; }
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        [ForeignKey("TripID")]
        public Guid TripID { get; set; }
        public virtual User User { get; set; }
        public virtual Trip Trip { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
