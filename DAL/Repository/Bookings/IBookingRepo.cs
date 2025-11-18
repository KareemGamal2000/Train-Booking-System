using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;


namespace Data.Repository.Bookings
{
     public interface IBookingRepo
    {
        Task<Booking> AddBookingAsync(Booking booking);
        Task<Booking?> GetBookingByIdAsync(Guid bookingId);
        Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid userId);
        Task<bool> UpdateBookingAsync(Booking booking);
    }
}
