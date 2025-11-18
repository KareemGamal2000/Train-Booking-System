using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TicketDtos
{
    public class TicketCreateDto
    {
        public int Seat_ID { get; set; }
        public long ClassID { get; set; }
        public decimal Price { get; set; }
    }
}
