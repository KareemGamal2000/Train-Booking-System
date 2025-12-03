using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.StationDtos
{
    public class StationReadDto
    {
        public string StationID { get; set; }
        public string StationNameAR { get; set; }
        public string StationNameEN { get; set; }
        public string? StationCode { get; set; }
        public string ShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
