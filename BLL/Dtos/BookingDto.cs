using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class BookingDto
    {
        public Guid BookingID { get; set; }
        public Guid UserID { get; set; }
        public int TripID { get; set; }

        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; }

        public int DepartureStopID { get; set; }
        public int ArrivalStopID { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
