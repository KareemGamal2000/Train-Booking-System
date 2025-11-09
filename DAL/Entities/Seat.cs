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
        public Guid Seat_ID { get; set; }= Guid.NewGuid();
        public string SeatNumber { get; set; }
        [ForeignKey("CoachID")]
        public Guid CoachID { get; set; }
        public virtual Coach Coach { get; set; }
    }
}
