using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class TrainTrackingDto
    {
        public Guid TrainId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Status { get; set; } // الحالة (On Time / Delayed / Arrived)
        public string ETA { get; set; }  // الوقت المتوقع للوصول
    }
}
