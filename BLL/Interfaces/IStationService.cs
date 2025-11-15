using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;

namespace Domain.Interfaces
{
    public interface IStationService
    {
        Task<IEnumerable<StationDto>> GetAllStationAsync();
        Task<StationDto?> GetStationByIdAsync(long id);
        Task<string> AddStationAsync(StationDto station);
        Task<string> UpdateStationAsync(StationDto station);
        Task<string> DeleteStationAsync(long id);
    }
}
