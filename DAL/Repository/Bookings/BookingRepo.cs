using Data.Context;
using Data.Models;
using Data.Models.Tickets;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository.Bookings
{
    public class BookingRepo : GenericRepo<Booking>, IBookingRepo
    {
        public BookingRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            await _dbSet.AddAsync(booking);
            return booking;
        }
        
        public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid userId)
        {
            // تحميل البيانات الأساسية مع أسماء المحطات
            string[] includes = new string[]
              {
                 "Tickets.Seat",
                 "Tickets.Class",
                 "DepartureStop.Station",
                 "ArrivalStop.Station"
              };
            return await GetAllWithOrderingAsync(
                filter: b => b.UserID == userId,
                include: includes,
                orderBy: q => q.OrderByDescending(b => b.BookingDate)
            );

        }
        
        public async Task<Booking?> GetBookingWithDetailsAsync(Guid bookingId)
        {
            string[] includes = new string[]
              {
                 "User",
                 "Trip.Train",
                 "DepartureStop.Station",
                 "ArrivalStop.Station",
                 "Tickets.Seat",
                 "Tickets.Class",
                 "Payments"
              };
            return await GetFirstOrDefaultAsync(
                filter: b => b.Booking_ID == bookingId,
                include: includes
            );
        }
        
        public async Task<IEnumerable<Booking>> GetConfirmedBookingsByUserIdAsync(Guid userId)
        {
            string[] includes = new string[]
              {
                 "Trip",
                 "Tickets.Seat",
                 "Tickets.Class",
              };
            return await GetAllWithOrderingAsync(
                filter: b => b.UserID == userId && b.BookingStatus == "Confirmed",
                include: includes,
                orderBy: q => q.OrderByDescending(b => b.BookingDate)
             );
        }

        public async Task<bool> CancelBookingAsync(Guid bookingId)
        {
            var booking = await _dbSet.FindAsync(bookingId);
            if (booking == null || booking.BookingStatus == "Cancelled")
                return false;

            booking.BookingStatus = "Cancelled";
            _dbSet.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _dbSet.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ConfirmBookingAsync(Guid bookingId)
        {
            var booking = await _dbSet.FindAsync(bookingId);
            if (booking == null)
                return false;

            booking.BookingStatus = "Confirmed";
            _dbSet.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}