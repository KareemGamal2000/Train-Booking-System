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
        Task<Data.Models.Train?> GetTrainByTrainNameAsync(string trainName);
    }
}
