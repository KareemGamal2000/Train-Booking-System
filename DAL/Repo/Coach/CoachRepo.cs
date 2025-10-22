using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Coach
{
    public class CoachRepo : ICoachRepo
    {
        private readonly ApplicationDbContext _context;

        public CoachRepo(ApplicationDbContext context)
        {
            _context = context;  
        }

        public async Task<IEnumerable<Entities.Coach>> GetCoachesAsync()
        {
            return await _context.Coaches.ToListAsync();
        }
        public async Task<Entities.Coach?> GetCoachByIdAsync(int id)
        {
            return await _context.Coaches.FirstOrDefaultAsync(c => c.Coach_ID == id);
        }
        public async Task<IEnumerable<Entities.Coach>> GetCoachesByTrainIdAsync(int trainId)
        {
            return await _context.Coaches.Where(c => c.TrainID == trainId).ToListAsync();
        }
        public async Task<IEnumerable<Entities.Coach>> GetCoachClass(Entities.Coach coach)
        {
            return await _context.Coaches.Include(c => c.Class).Where(c => c.Coach_ID == coach.Coach_ID).ToListAsync();
        }
        public async Task<string> AddCoachAsync(Entities.Coach coach)
        {
            await _context.Coaches.AddAsync(coach);
            await _context.SaveChangesAsync();
            return "Coach added successfully";
        }
        public async Task<string> UpdateCoachAsync(Entities.Coach coach)
        {
            _context.Coaches.Update(coach);
            await _context.SaveChangesAsync();
            return "Coach updated successfully";
        }
        public async Task<string> DeleteCoachAsync(int id)
        {
            var coach = await _context.Coaches.FirstOrDefaultAsync(c => c.Coach_ID == id);
            if (coach != null)
            {
                _context.Coaches.Remove(coach);
                await _context.SaveChangesAsync();
                return "Coach deleted successfully";
            }
            return "Coach not found";
        }
       


    }
}
