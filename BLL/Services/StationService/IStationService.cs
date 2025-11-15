using Domain.Dtos;
using Domain.Dtos.StationDtos;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace Domain.Services.StationService
{
    public interface IStationService
    {
        Task<IEnumerable<StationReadDto>> GetAllStationAsync();
        Task<StationReadDto> GetStationByIdAsync(long stationId);

        Task<StationReadDto?> GetStationBynameAsync(string stationname);

        Task<StationReadDto> CreateStationAsync(StationCreateDto stationDto);

        Task<bool> UpdateStationAsync(string stationname, StationUpdateDto stationDto);

        Task<bool> DeleteStationAsync(long stationId);
    }
}
