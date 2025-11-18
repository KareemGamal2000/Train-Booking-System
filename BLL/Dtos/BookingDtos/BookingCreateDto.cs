using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDto
{
    public class BookingCreateDto
    {
        public Guid UserID { get; set; }
        public int TripID { get; set; }
        public int DepartureStopID { get; set; }
        public int ArrivalStopID { get; set; }
        public decimal PricePerSeat { get; set; }
        public List<BookingSeatSelectionDto>? Seats { get; set; }
    }
}
