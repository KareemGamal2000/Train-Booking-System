using Data.Repository.MainRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.TrainCoach
{
    public interface ITrainCoachRepo : IGenericRepo<Data.Models.TrainCoach>
    {
        Task<IEnumerable<Data.Models.TrainCoach>> GetTrainCoachesByTrainIdAsync(long trainId);

        Task<IEnumerable<Data.Models.TrainCoach>> GetTrainCoachesWithDetailsByTrainIdAsync(long trainId);

        Task<Data.Models.TrainCoach?> GetTrainCoachWithDetailsByIdAsync(int trainCoachId);
    }
}
