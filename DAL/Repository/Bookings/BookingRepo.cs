using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;


namespace Data.Repository.Bookings
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext _context;

        public BookingRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Tickets)      // ← صح
                .FirstOrDefaultAsync(b => b.Booking_ID == bookingId);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid userId)
        {
            return await _context.Bookings
                .Where(b => b.UserID == userId)   // ← صح
                .Include(b => b.Tickets)
                .ToListAsync();
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}