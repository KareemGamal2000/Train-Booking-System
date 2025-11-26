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
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public decimal? Price { get; set; }
    }
}
