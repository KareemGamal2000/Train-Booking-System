using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TrainDtos
{
    public class TrainCoachDto
    {
        public string CoachID { get; set; }
        public string CoachType { get; set; } 
        public string ClassNameAR { get; set; } 

        public string ClassNameEN { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsActive { get; set; }
    }
}
