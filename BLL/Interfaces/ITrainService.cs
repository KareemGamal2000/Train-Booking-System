using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITrainService
    {
        Task<IEnumerable<TrainDto>> GetAllAsync();
        Task<TrainDto?> GetByIdAsync(int id);
        Task<string> AddAsync(TrainDto train);
        Task<string> UpdateAsync(TrainDto train);
        Task<string> DeleteAsync(int id);
    }
}
