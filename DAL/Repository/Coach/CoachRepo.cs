using Data.Context;
using Data.Models;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Coach
{
    public class CoachRepo : GenericRepo<Data.Models.Coach> , ICoachRepo
    {
        public CoachRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Data.Models.Coach>> GetCoachesByClassIdAsync(long classId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.ClassId == classId)
                .Include(c => c.Class) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Data.Models.Coach>> GetCoachesByClassNameAsync(string classname)
        {
            var stationNameLower = classname.ToLower();

            return await _dbSet
                .AsNoTracking()
                .Include(c => c.Class)
                .Where(c => c.Class != null &&( c.Class.ClassNameAR.ToLower().Contains(stationNameLower) ||
                   c.Class.ClassNameEN.ToLower().Contains(stationNameLower)))
                .ToListAsync();
        }


        public async Task<Data.Models.Coach?> GetCoachWithSeatsAndClassAsync(long coachId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(c => c.Class) 
                .Include(c => c.Seats) 
                .FirstOrDefaultAsync(c => c.Coach_ID == coachId);
        }
    }
}
