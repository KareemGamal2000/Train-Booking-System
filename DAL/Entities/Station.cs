using Data.Entities.Trips;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Station
    {
        [Key]
        public long StationID { get; set; } 

        public string StationNameEN { get; set; }

        public string StationNameAR { get; set; }

        public string StationCode { get; set; }

        public string ShortName { get; set; } 
       
        public bool IsActive { get; set; }

        // خاصية ملاحة جديدة: ربط المحطة بجميع التوقفات التي تحدث فيها (TripStops)
        public virtual ICollection<TripStop> Stops { get; set; } = new HashSet<TripStop>();
        public virtual ICollection<Trip> DepartureTrips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<Trip> ArrivalTrips { get; set; } = new HashSet<Trip>();

    }
}
