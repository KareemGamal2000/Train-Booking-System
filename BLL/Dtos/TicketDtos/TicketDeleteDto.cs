using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.TicketDtos
{
    public class TicketDeleteDto
    {
        public Guid Ticket_ID { get; set; }
        public string? Reason { get; set; }
    }
}
