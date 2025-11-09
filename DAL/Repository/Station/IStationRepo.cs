using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Station
{
    public interface IStationRepo
    {
        Task<IEnumerable<Entities.Station>> GetAllStationsAsync();
        Task<Entities.Station?> GetStationByIdAsync(Guid id);
        Task<Entities.Station?> GetStationByCodeAsync(string stationCode);
        Task<IEnumerable<Entities.Station>> GetStationsByNameAsync(string name);
        Task<string> AddStationAsync(Entities.Station station);
        Task<string> UpdateStationAsync(Entities.Station station);
        Task<string> DeleteStationAsync(Guid id);
    }
}
