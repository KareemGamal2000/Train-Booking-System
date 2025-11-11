using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class TrainCoach
    {
        [Key]
        public long TrainCoach_ID { get; set; }

        [ForeignKey("CoachID")]
        public long CoachID { get; set; }
        public virtual Coach Coach { get; set; }

        [ForeignKey("TrainID")]
        public long TrainID { get; set; }
        public virtual Train Train { get; set; }
        public int CoachNumber { get; set; } 
        public int AvailableSeats { get; set; }

        public bool IsActive { get; set; } // حالة العربة في هذا القطار
    
}
}
