using Data.Context;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.TrainCoach
{
    public class TrainCoachRepo : GenericRepo<Data.Models.TrainCoach>, ITrainCoachRepo
    {
        public TrainCoachRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Data.Models.TrainCoach>> GetTrainCoachesByTrainIdAsync(long trainId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(tc => tc.TrainID == trainId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Data.Models.TrainCoach>> GetTrainCoachesWithDetailsByTrainIdAsync(long trainId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(tc => tc.TrainID == trainId)
                .Include(tc => tc.Train)
                .Include(tc => tc.Coach)
                .ToListAsync();
        }
        public async Task<Data.Models.TrainCoach?> GetTrainCoachWithDetailsByIdAsync(int trainCoachId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(tc => tc.TrainCoach_ID == trainCoachId)
                // تضمين بيانات القطار
                .Include(tc => tc.Train)
                // تضمين بيانات العربة
                .Include(tc => tc.Coach)
                .FirstOrDefaultAsync();
        }

    }
}
