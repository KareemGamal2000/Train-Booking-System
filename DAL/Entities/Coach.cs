using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Coach
    {
        [Key]
        public int Coach_ID { get; set; }
        public string CoachNumber { get; set; }
        public int TotalSeats { get; set; }
        public bool IsActive { get; set; }
        public bool NoSeatsFlag { get; set; }
        [ForeignKey("TrainID")]
        public int TrainID { get; set; }
        public virtual Train Train { get; set; }
        [ForeignKey("ClassId")]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();


    }
}
