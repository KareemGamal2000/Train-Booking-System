using Data.Models;
using Data.Models.Tickets;
using Data.Repository.MainRepo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Bookings
{
    public interface IBookingRepo : IGenericRepo<Booking>
    {
        Task<Booking> AddBookingAsync(Booking booking);
        Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid userId);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<Booking?> GetBookingWithDetailsAsync(Guid bookingId);
        Task<IEnumerable<Booking>> GetConfirmedBookingsByUserIdAsync(Guid userId);
        Task<bool> CancelBookingAsync(Guid bookingId);
        Task<bool> ConfirmBookingAsync(Guid bookingId);
    }
}
