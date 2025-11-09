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
    public interface IStationService
    {
        Task<IEnumerable<StationDto>> GetAllAsync();
        Task<StationDto?> GetByIdAsync(Guid id);
        Task<string> AddStationAsync(StationDto station);
        Task<string> UpdateStationAsync(StationDto station);
        Task<string> DeleteStationAsync(Guid id);
    }
}
