using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class TicketDto
    {
        public long SeatID { get; set; }
        public long ClassID { get; set; }
        public decimal Price { get; set; }
    }
}
