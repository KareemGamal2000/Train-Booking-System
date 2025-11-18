using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TicketDtos
{
    public class TicketReadDto
    {
        public Guid Ticket_ID { get; set; }
        public string TicketReference { get; set; }

        public int Seat_ID { get; set; }
        public int SeatNumber { get; set; }

        public long ClassID { get; set; }
        public string ClassName { get; set; }

        public decimal Price { get; set; }
    }
}
