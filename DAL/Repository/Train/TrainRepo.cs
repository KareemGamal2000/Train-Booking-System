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

        public TrainRepo(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Data.Models.Train>> GetAllTrainsWithClassesAsync()
        {
            return await GetAllAsync(filter: null, include: new string[] { "TrainCoaches.Coach.Class" });
            
        }

        public async Task<Data.Models.Train?> GetTrainWithClassesByIdAsync(long trainId)
        {
            return await GetFirstOrDefaultAsync(
                filter: t => t.TrainID == trainId,
                include: new string[] { "TrainCoaches.Coach.Class" }
            );

        }

        public async Task<Data.Models.Train?> GetTrainByTrainNameAsync(string trainName)
        {
            return await GetFirstOrDefaultAsync(
                filter: t => t.TrainName == trainName,
                include: new string[] { "TrainCoaches.Coach.Class" }
            );

        }



    }
}
