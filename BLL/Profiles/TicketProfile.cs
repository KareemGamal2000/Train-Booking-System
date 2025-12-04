using Data.Models.Tickets;
using Domain.Dtos.TicketDtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Profiles
{
    public static class TicketProfile
    {
        public static TicketReadDto ToTicketReadDto(this Ticket ticket)
        {
            if (ticket == null) return null;

            return new TicketReadDto
            {
                Ticket_ID = ticket.Ticket_ID,
                TicketReference = ticket.TicketReference,
                Seat_ID = ticket.SeatID,
                SeatNumber = ticket.Seat?.SeatNumber ?? 0,
                ClassID = ticket.ClassID,
                ClassName = ticket.Class?.ClassNameAR ?? "غير محدد",
                Price = ticket.Price
            };
        }

        public static TicketSummaryDto ToTicketSummaryDto(this Ticket ticket)
        {
            if (ticket == null) return null;

            return new TicketSummaryDto
            {
                Ticket_ID = ticket.Ticket_ID,
                TicketReference = ticket.TicketReference,
                SeatNumber = ticket.Seat?.SeatNumber ?? 0,
                Price = ticket.Price
            };
        }

        public static Ticket ToTicketModel(this TicketCreateDto dto, Guid bookingId)
        {
            if (dto == null) return null;

            return new Ticket
            {
                Ticket_ID = Guid.NewGuid(),
                Booking_ID = bookingId,
                SeatID = dto.Seat_ID,
                ClassID = dto.ClassID,
                Price = dto.Price,
                TicketReference = $"T-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
            };
        }

        public static IEnumerable<TicketSummaryDto> ToTicketSummaryDtoList(this IEnumerable<Ticket> tickets)
        {
            if (tickets == null) return Enumerable.Empty<TicketSummaryDto>();

            return tickets.Select(t => t.ToTicketSummaryDto()).ToList();
        }
    }
}
