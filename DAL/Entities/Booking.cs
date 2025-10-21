using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Booking
    {
        [Key]
        public int Booking_ID { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; } // "Confirmed", "Cancelled"
        public decimal TotalPrice { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        [ForeignKey("TripID")]
        public int TripID { get; set; }
        public virtual User User { get; set; }
        public virtual Trip Trip { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
