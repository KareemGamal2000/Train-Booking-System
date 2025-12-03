using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TrainDtos
{
    public class TrainWithClassesDto
    {
        public string ClassID { get; set; }
        public string ClassNameAR { get; set; }
        public string ClassNameEN { get; set; }
        public int NumberOfCoaches { get; set; }
        public int TotalAvailableSeats { get; set; }
    }
}
