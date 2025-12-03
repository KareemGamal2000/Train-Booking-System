using Data.Context;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            return await GetAllAsync(filter: tc => tc.TrainID == trainId, include: null);
        }
        public async Task<IEnumerable<Data.Models.TrainCoach>> GetTrainCoachesWithDetailsByTrainIdAsync(long trainId)
        {
            return await GetAllAsync(filter: tc => tc.TrainID == trainId, include: new string[] { "Train", "Coach" });
        }
        public async Task<Data.Models.TrainCoach?> GetTrainCoachWithDetailsByIdAsync(int trainCoachId)
        {
            return await GetFirstOrDefaultAsync(filter: tc => tc.TrainCoach_ID == trainCoachId, include: new string[] { "Train", "Coach" });
        }

    }
}
