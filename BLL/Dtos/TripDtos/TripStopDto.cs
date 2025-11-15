using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TripDtos
{
    public class TripStopDto
    {

            public int TripStopID { get; set; }
            public long StationID { get; set; }
            public string StationNameAR { get; set; } 
            public int StopSequence { get; set; } 
            public TimeSpan? ArrivalTime { get; set; }
            public TimeSpan? DepartureTime { get; set; }
            public decimal? DistanceFromStartKM { get; set; }

    }
}
