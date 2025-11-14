using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Trips;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.TripStops
{
    public class TripStopRepo : ITripStopRepo
    {
        private readonly ApplicationDbContext _context;

        public TripStopRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TripStop> GetById(int id)
        {
            return await _context.TripStops.FindAsync(id);
        }

        public async Task<List<TripStop>> GetAll()
        {
            return await _context.TripStops.ToListAsync();
        }

        public async Task Add(TripStop entity)
        {
            await _context.TripStops.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TripStop entity)
        {
            _context.TripStops.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var stop = await _context.TripStops.FindAsync(id);
            if (stop != null)
            {
                _context.TripStops.Remove(stop);
                await _context.SaveChangesAsync();
            }
        }
    }
}