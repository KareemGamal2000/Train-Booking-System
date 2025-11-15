using Data.Context;
using Data.Models;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Train
{
    public class TrainRepo : GenericRepo<Data.Models.Train>, ITrainRepo
    {
        private readonly ApplicationDbContext _context;

        public TrainRepo(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Data.Models.Train>> GetAllTrainsWithClassesAsync()
        {
            return await _dbSet
                .Include(t => t.TrainCoaches) 
                    .ThenInclude(tc => tc.Coach) 
                        .ThenInclude(c => c.Class) 
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Data.Models.Train?> GetTrainWithClassesByIdAsync(long trainId)
        {
            return await _dbSet
                .Where(t => t.TrainID == trainId)
                .Include(t => t.TrainCoaches) 
                    .ThenInclude(tc => tc.Coach) 
                        .ThenInclude(c => c.Class) 
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Data.Models.Train?> GetTrainByTrainNameAsync(string trainName)
        {
            return await _dbSet
                .Where(t => t.TrainName == trainName)
                .Include(t => t.TrainCoaches)
                    .ThenInclude(tc => tc.Coach)
                        .ThenInclude(c => c.Class)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }



    }
}
