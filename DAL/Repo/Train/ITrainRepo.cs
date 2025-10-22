using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Train
{
    public interface ITrainRepo
    {
        Task<IEnumerable<Entities.Train>> GetAllTrainsAsync();
        Task<Entities.Train?> GetTrainByIdAsync(int id);
        Task<Entities.Train?> GetTrainByTrainNumberAsync(string trainNumber);
        Task<string> AddTrainAsync(Entities.Train train);

        Task<string> UpdateTrainAsync(Entities.Train train);
        Task<string> DeleteTrainAsync(int id);
    }
}
