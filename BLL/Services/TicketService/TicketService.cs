using AutoMapper;
using Data.Models.Tickets;
using Domain.Dtos.TicketDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TicketService
{
    public class TicketService : ITicketService

    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public TicketService(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TicketReadDto> GetTicketByIdAsync(Guid ticketId)
        {
            var ticket = await _context.Set<Ticket>()
                .Include(t => t.Seat)
                .Include(t => t.Class)
                .FirstOrDefaultAsync(t => t.Ticket_ID == ticketId);

            return _mapper.Map<TicketReadDto>(ticket);
        }

        public async Task<IEnumerable<TicketSummaryDto>> GetTicketsByBookingIdAsync(Guid bookingId)
        {
            var tickets = await _context.Set<Ticket>()
                .Include(t => t.Seat)
                .Where(t => t.Booking_ID == bookingId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TicketSummaryDto>>(tickets);
        }

        public async Task<TicketReadDto> CreateTicketAsync(Guid bookingId, TicketCreateDto dto)
        {
            var ticket = _mapper.Map<Ticket>(dto);
            ticket.Booking_ID = bookingId;
            _context.Add(ticket);
            await _context.SaveChangesAsync();

            return _mapper.Map<TicketReadDto>(ticket);
        }
        public async Task<bool> DeleteTicketAsync(TicketDeleteDto dto)
        {
            var ticket = await _context.Set<Ticket>().FindAsync(dto.Ticket_ID);
            if (ticket == null) return false;

            _context.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}