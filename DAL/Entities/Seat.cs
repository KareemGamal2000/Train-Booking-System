using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Seat
    {
        [Key]
        public int Seat_ID { get; set; }
        public string SeatNumber { get; set; }
        [ForeignKey("CoachID")]
        public int CoachID { get; set; }
        public virtual Coach Coach { get; set; }
    }
}
