using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Station
    {
        [Key]
        public int Station_ID { get; set; }
        public string StationNameEN { get; set; }
        public string StationNameAR { get; set; }
        public string StationCode { get; set; }

        public string City { get; set; }

        public virtual ICollection<Trip> DepartureTrips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<Trip> ArrivalTrips { get; set; } = new HashSet<Trip>();

    }
}
