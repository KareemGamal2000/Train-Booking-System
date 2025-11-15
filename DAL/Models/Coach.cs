using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Coach
    {
        [Key]
        public long Coach_ID { get; set; } 
        public int TotalSeats { get; set; }
        public bool IsSeatless { get; set; } //هل الحجز بدون تحديد كرسى
        public string CoachType { get; set; } // نوع العربة من حيث بها تحديد كراسى او لا

        [ForeignKey("ClassId")]
        public long ClassId { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<TrainCoach> TrainCoaches { get; set; } = new HashSet<TrainCoach>();
        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();


    }
}
