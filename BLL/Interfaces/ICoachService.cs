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
    public interface ICoachService
    {
        Task<IEnumerable<CoachDto>> GetAllAsync();
        Task<CoachDto?> GetByIdAsync(int id);
        Task<IEnumerable<CoachDto>> GetByTrainIdAsync(int trainId);
        Task<string> AddAsync(CoachDto coach);
        Task<string> UpdateAsync(CoachDto coach);
        Task<string> DeleteAsync(int id);
    }
}
