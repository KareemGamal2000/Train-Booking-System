using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDtos
{
    public class AvailableSeatsDto
    {
        public int TripID { get; set; }
        public long ClassID { get; set; }
        public int TotalAvailableSeats { get; set; }
        public List<SeatDto> Seats { get; set; } = new List<SeatDto>();
    }
}