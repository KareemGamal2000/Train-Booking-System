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
    public interface IStationService
    {
        Task<IEnumerable<StationDto>> GetAllAsync();
        Task<StationDto?> GetByIdAsync(int id);
        Task<string> AddStationAsync(StationDto station);
        Task<string> UpdateStationAsync(StationDto station);
        Task<string> DeleteStationAsync(int id);
    }
}
