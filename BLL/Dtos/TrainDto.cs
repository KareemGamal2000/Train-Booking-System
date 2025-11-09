using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class TrainDto
    {
        public int Train_ID { get; set; }
        public string TrainNumber { get; set; }
        public string TrainName { get; set; }
        public string TrainType { get; set; }
        public int TotalSeats { get; set; }
    }
}
