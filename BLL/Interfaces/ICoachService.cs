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
    public interface ICoachService
    {
        Task<IEnumerable<CoachDto>> GetAllAsync();
        Task<CoachDto?> GetByIdAsync(long id);
        Task<string> AddAsync(CoachDto coach);
        Task<string> UpdateAsync(CoachDto coach);
        Task<string> DeleteAsync(long id);
    }
}
