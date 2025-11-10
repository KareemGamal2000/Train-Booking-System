using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Coach
    {
        [Key]
        public long Coach_ID { get; set; } 
        public string CoachNumber { get; set; }
        public int TotalSeats { get; set; }
        public bool IsActive { get; set; }
        public bool NoSeatsFlag { get; set; }
        [ForeignKey("TrainID")]
        public long TrainID { get; set; }
        public virtual Train Train { get; set; }

        [ForeignKey("ClassId")]
        public long ClassId { get; set; }
        public virtual Class Class { get; set; }

        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();


    }
}
