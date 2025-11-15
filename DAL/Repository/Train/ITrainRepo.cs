using Data.Repository.MainRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Train
{
    public interface ITrainRepo : IGenericRepo<Data.Models.Train>
    {
        Task<IEnumerable<Data.Models.Train>> GetAllTrainsWithClassesAsync();
        Task<Data.Models.Train?> GetTrainWithClassesByIdAsync(long trainId);
        Task<Data.Models.Train?> GetTrainByTrainNameAsync(string trainName);
    }
}
