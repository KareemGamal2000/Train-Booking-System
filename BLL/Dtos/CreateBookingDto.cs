using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class CreateBookingDto
    {
        public Guid UserID { get; set; }
        public int TripID { get; set; }

        public int DepartureStopID { get; set; }
        public int ArrivalStopID { get; set; }

        public List<TicketDto> Tickets { get; set; }
    }
}
