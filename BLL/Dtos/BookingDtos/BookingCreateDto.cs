using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDto
{
    public class BookingCreateDto
    {
        public int TripID { get; set; }
        public int DepartureStopID { get; set; }
        public int ArrivalStopID { get; set; }
        public long ClassID { get; set; }
        public int NumberOfSeats { get; set; }
        public List<int>? SelectedSeatIDs { get; set; }
    }
}
