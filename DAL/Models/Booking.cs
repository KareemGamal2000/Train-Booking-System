using Data.Entities;
using Data.Entities.Tickets;
using Data.Entities.Trips;
using Data.Models.Trips;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Booking
    {
        [Key]
        public Guid Booking_ID { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; } // "Confirmed", "Cancelled"

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("TripID")]
        public int TripID { get; set; }
        public virtual Trip Trip { get; set; }

        [ForeignKey("DepartureStopID")]
        public int DepartureStopID { get; set; }
        public virtual TripStop DepartureStop { get; set; }

        [ForeignKey("ArrivalStopID")]
        public int ArrivalStopID { get; set; }
        public virtual TripStop ArrivalStop { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
