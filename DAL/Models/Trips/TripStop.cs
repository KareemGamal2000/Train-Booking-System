using Data.Models;
using Data.Models.Trips;
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
    [Index(nameof(TripID), nameof(StopSequence), IsUnique = true, Name = "IX_TripStops_TripID_StopSequence")]
    [Index(nameof(TripID), nameof(StationID), Name = "IX_TripStops_TripID_StationID")]
    [Index(nameof(StationID), Name = "IX_TripStops_StationID")]
    public class TripStop
    {
        [Key]
        public int TripStopID { get; set; } // المفتاح الأساسي لتوقف الرحلة

        [ForeignKey("TripID")]
        public int TripID { get; set; }
        public virtual Trip Trip { get; set; }

        [ForeignKey("StationID")]
        public long? StationID { get; set; }
        public virtual Station Station { get; set; }

        // ترتيب المحطة فى مسار الرحلة
        public int StopSequence { get; set; }

        // arrival time to station may be null when station is first
        public TimeSpan? ArrivalTime { get; set; }

        // Depaerture time froom station may be null when station is end
        public TimeSpan? DepartureTime { get; set; }

        // المسافة المقطوعة من نقطة بداية الرحلة)
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DistanceFromStartKM { get; set; }

    }
}
