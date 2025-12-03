using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDtos
{
    public class SeatDto
    {
        public int SeatID { get; set; }
        public int SeatNumber { get; set; }
        public long CoachID { get; set; }
        public string CoachType { get; set; }
        public bool IsAvailable { get; set; }
    }
}