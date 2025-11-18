using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Seat?> GetSeatByIdAsync(int seatId)
        {
            return await _context.Seats.FirstOrDefaultAsync(s => s.SeatID == seatId);
        }
    }
}