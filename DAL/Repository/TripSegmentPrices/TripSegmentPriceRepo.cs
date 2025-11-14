using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Trips;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.TripSegmentPrices
{
    public class TripSegmentPriceRepo : ITripSegmentPriceRepo
    {
        private readonly ApplicationDbContext _context;

        public TripSegmentPriceRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TripSegmentPrice> GetById(int id)
        {
            return await _context.TripSegmentPrices.FindAsync(id);
        }

        public async Task<List<TripSegmentPrice>> GetAll()
        {
            return await _context.TripSegmentPrices.ToListAsync();
        }

        public async Task Add(TripSegmentPrice entity)
        {
            await _context.TripSegmentPrices.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TripSegmentPrice entity)
        {
            _context.TripSegmentPrices.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var seg = await _context.TripSegmentPrices.FindAsync(id);
            if (seg != null)
            {
                _context.TripSegmentPrices.Remove(seg);
                await _context.SaveChangesAsync();
            }
        }
    }
}