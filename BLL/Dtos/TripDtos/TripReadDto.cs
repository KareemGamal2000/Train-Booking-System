using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TripDtos
{
    public class TripReadDto
    {
        public int Trip_ID { get; set; }
        public long TrainID { get; set; }
        public string TrainName { get; set; }
        public long DepartureStationID { get; set; }
        public string DepartureStationNameAR { get; set; }

        public long ArrivalStationID { get; set; }
        public string ArrivalStationNameAR { get; set; }

        public ICollection<TripStopDto> Stops { get; set; } = new List<TripStopDto>();

        public ICollection<TripSegmentPriceDto> SegmentPrices { get; set; } = new List<TripSegmentPriceDto>();
    }
}
