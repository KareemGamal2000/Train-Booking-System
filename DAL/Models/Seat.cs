using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Seat
    {

        [Key]
        public int Seat_ID { get; set; }
        public int SeatNumber { get; set; }

        [ForeignKey("CoachID")]
        public long CoachID { get; set; }
        public virtual Coach Coach { get; set; }
    }
}
