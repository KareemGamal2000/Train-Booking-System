using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDtos
{
    public class BookingSummaryDto
    {
        public Guid BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public int TripID { get; set; }
        public List<int> Seats { get; set; }
        public int NumberOfSeats { get; set; }

    }
}
