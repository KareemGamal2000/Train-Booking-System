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


        public async Task<Data.Models.Train?> GetTrainByTrainNameAsync(string trainName)
        {
            return await GetFirstOrDefaultAsync(t => t.TrainName == trainName);
        }



    }
}
