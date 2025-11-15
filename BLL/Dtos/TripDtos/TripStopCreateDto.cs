using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TripDtos
{
    public class TripStopCreateDto
    {
        [Required]
        public long StationID { get; set; }

        [Required]
        public int StopSequence { get; set; }

        public TimeSpan? ArrivalTime { get; set; } // قد يكون null في المحطة الأولى
        public TimeSpan? DepartureTime { get; set; } // قد يكون null في المحطة الأخيرة
        public decimal? DistanceFromStartKM { get; set; }
    }
}
