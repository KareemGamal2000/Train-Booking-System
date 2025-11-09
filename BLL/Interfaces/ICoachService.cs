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
        Task<CoachDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<CoachDto>> GetByTrainIdAsync(Guid trainId);
        Task<string> AddAsync(CoachDto coach);
        Task<string> UpdateAsync(CoachDto coach);
        Task<string> DeleteAsync(Guid id);
    }
}
