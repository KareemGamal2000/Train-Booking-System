using Data.Repository.MainRepo;
using System;
using System.Threading.Tasks;

namespace Data.Repository.Ticket
{
    public interface ITicketRepo : IGenericRepo<Data.Models.Tickets.Ticket>
    {
        Task<Data.Models.Tickets.Ticket?> GetTicketByIdWithIncludesAsync(Guid ticketId);

        Task<IEnumerable<Data.Models.Tickets.Ticket>> GetTicketsByBookingIdWithIncludesAsync(Guid bookingId);

        Task<Data.Models.Tickets.Ticket?> GetTicketWithIncludesAsync(Guid ticketId);
        Task<bool> IsSeatAvailableAsync(int seatId, int tripId);

        Task<IEnumerable<int>> GetBookedSeatIdsAsync(int tripId, long classId, int departureStopId, int arrivalStopId);
    }
}