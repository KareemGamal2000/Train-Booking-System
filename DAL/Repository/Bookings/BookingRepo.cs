using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Repository.Bookings
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext _context;

        public BookingRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> GetById(Guid id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task Add(Booking entity)
        {
            await _context.Bookings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Booking entity)
        {
            _context.Bookings.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
