using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Train
{
    public class TrainRepo : ITrainRepo
    {
        private readonly ApplicationDbContext _context;

        public TrainRepo(ApplicationDbContext trainrepo)
        {
            _context = trainrepo;
        }

        public async Task<IEnumerable<Entities.Train>> GetAllTrainsAsync()
        {
            return await _context.Trains.ToListAsync();
        }
        public async Task<Entities.Train?> GetTrainByIdAsync(int id)
        {
            return await _context.Trains.FindAsync(id);
        }
        public async Task<Entities.Train?> GetTrainByTrainNumberAsync(string trainNumber)
        {
            return await _context.Trains.FirstOrDefaultAsync(t => t.TrainNumber == trainNumber);
        }
        public async Task<string> AddTrainAsync(Entities.Train train)
        {
            await _context.Trains.AddAsync(train);
            await _context.SaveChangesAsync();
            return "Train added successfully";
        }
        public async Task<string> UpdateTrainAsync(Entities.Train train)
        {
            _context.Trains.Update(train);
            await _context.SaveChangesAsync();
            return "Train updated successfully";
        }
        public async Task<string> DeleteTrainAsync(int id)
        {
            var train = await _context.Trains.FindAsync(id);
            if (train != null)
            {
                _context.Trains.Remove(train);
                await _context.SaveChangesAsync();
                return "Train deleted successfully";
            }
            return "Train not found";
        }



    }
}
