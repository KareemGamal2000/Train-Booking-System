using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITrainService
    {
        Task<IEnumerable<TrainDto>> GetAllAsync();
        Task<TrainDto?> GetByIdAsync(Guid id);
        Task<string> AddAsync(TrainDto train);
        Task<string> UpdateAsync(TrainDto train);
        Task<string> DeleteAsync(Guid id);
    }
}
