using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Entities.Trips
{
    public class TripSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }

        public DateTime TripDate { get; set; }
    }
}
