using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TripDtos
{
    public class TripSegmentPriceDto
    {
        public int SegmentPriceID { get; set; }
        public long ClassID { get; set; }
        public string ClassNameAR { get; set; }
        public int StartStopID { get; set; }
        public int EndStopID { get; set; }
        public decimal Price { get; set; }
    }
}
