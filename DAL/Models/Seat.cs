using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatID { get; set; }

        public int SeatNumber { get; set; }

        [ForeignKey("CoachID")]
        public long CoachID { get; set; }
        public virtual Coach Coach { get; set; }

        
    }
}
