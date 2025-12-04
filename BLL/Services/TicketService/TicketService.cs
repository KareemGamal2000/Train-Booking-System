using Data.Models.Tickets;
using Data.Repository.MainRepo;
using Data.Repository.UnitOfWork;
using Domain.Dtos.TicketDtos;
using Domain.Profiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ===================== Get Ticket By ID =======================
        public async Task<TicketReadDto> GetTicketByIdAsync(Guid ticketId)
        {
            var ticket = await _unitOfWork.Ticket.GetTicketByIdWithIncludesAsync(ticketId);

            if (ticket == null)
                return null;

            return ticket.ToTicketReadDto();
        }

        // ===================== Get Tickets By Booking ID =======================
        public async Task<IEnumerable<TicketSummaryDto>> GetTicketsByBookingIdAsync(Guid bookingId)
        {
            var tickets = await _unitOfWork.Ticket.GetTicketsByBookingIdWithIncludesAsync(bookingId);

            return tickets.ToTicketSummaryDtoList();
        }

        // ===================== Create Ticket =======================
        public async Task<TicketReadDto> CreateTicketAsync(Guid bookingId, TicketCreateDto dto)
        {
            var ticket = dto.ToTicketModel(bookingId);

            await _unitOfWork.Ticket.AddAsync(ticket);

            await _unitOfWork.SaveChangesAsync();

            var savedTicket = await _unitOfWork.Ticket.GetTicketWithIncludesAsync(ticket.Ticket_ID);

            return savedTicket.ToTicketReadDto();
        }

        // ===================== Delete Ticket =======================
        public async Task<bool> DeleteTicketAsync(TicketDeleteDto dto)
        {
            var ticket = await _unitOfWork.Ticket.GetByIdAsync(dto.Ticket_ID);
            if (ticket == null)
                return false;

            _unitOfWork.Ticket.Delete(ticket);

            return await _unitOfWork.SaveChangesAsync();
        }
    }
}