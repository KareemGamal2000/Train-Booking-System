using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Trips
{
   
    [EntityTypeConfiguration(typeof(TripConfigration))]    [Index(nameof(DepartureStationID), nameof(ArrivalStationID), IsUnique = false, Name = "IX_Trips_Departure_Arrival")]
    public class Trip
    {
        [Key]
        public int TripID { get; set; } 

        [ForeignKey("TrainID")]
        [Required]
        public long TrainID { get; set; }
        public virtual Train Train { get; set; }

        // محطة الاقلاع الاولى
        [ForeignKey("DepartureStationID")]
        [Required]
        public long? DepartureStationID { get; set; }
        public virtual Station Departure_Station { get; set; }

        // محطة الوصول الرئيسية للرحلة 
        [ForeignKey("ArrivalStationID")]
        [Required]
        public long? ArrivalStationID { get; set; }
        public virtual Station Arrival_Station { get; set; }

        // ربط قائمة محطات التوقف 
        public virtual ICollection<TripStop> Stops { get; set; } = new HashSet<TripStop>();
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

        public virtual ICollection<TripSegmentPrice> SegmentPrices { get; set; } = new HashSet<TripSegmentPrice>();


    }
}
