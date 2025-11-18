using Domain.Dtos.TicketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TicketService
{
    public interface ITicketService
    {
        Task<TicketReadDto> GetTicketByIdAsync(Guid ticketId);
        Task<IEnumerable<TicketSummaryDto>> GetTicketsByBookingIdAsync(Guid bookingId);
        Task<TicketReadDto> CreateTicketAsync(Guid bookingId, TicketCreateDto dto);
        Task<bool> DeleteTicketAsync(TicketDeleteDto dto);
    }
}
