using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Seats
{
    public class SeatRepo : ISeatRepo
    {
        private readonly ApplicationDbContext _context;

        public SeatRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Seat> GetSeatByIdAsync(long seatId)
        {
            return await _context.Seats.FirstOrDefaultAsync(s => s.Seat_ID == seatId);
        }

        public async Task MarkReservedAsync(long seatId)
        {
            var seat = await _context.Seats.FirstOrDefaultAsync(s => s.Seat_ID == seatId);

            if (seat != null)
            {
                seat.IsReserved = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Seat> GetById(Guid id)
        {
            return await _context.Seats.FindAsync(id);
        }

        public async Task<List<Seat>> GetAll()
        {
            return await _context.Seats.ToListAsync();
        }

        public async Task Add(Seat entity)
        {
            await _context.Seats.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Seat entity)
        {
            _context.Seats.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat != null)
            {
                _context.Seats.Remove(seat);
                await _context.SaveChangesAsync();
            }
        }
    }
}
