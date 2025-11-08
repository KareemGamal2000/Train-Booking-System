using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class CoachDto
    {
        public int Coach_ID { get; set; }
        public string CoachNumber { get; set; }
        public string ClassType { get; set; }
        public int TrainID { get; set; }
    }
}
