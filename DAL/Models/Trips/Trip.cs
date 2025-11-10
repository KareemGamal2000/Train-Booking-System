using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Trips
{
   
    [EntityTypeConfiguration(typeof(TripConfigration))]
    public class Trip
    {
        [Key]
        public Guid Trip_ID { get; set; } = Guid.NewGuid();
        [ForeignKey("TrainID")]
        public long TrainID { get; set; }
        public long DepartureStationID { get; set; }   // محطة الاقلاع
        public long ArrivalStationID { get; set; }   // محطة الوصول
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal TicketPrice { get; set; }
        public virtual Train Train { get; set; }
        public virtual Station Departure_Station { get; set; }
        public virtual Station Arrival_Station { get; set; }
        public int AvailableSeatsTotal { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

    }
}
