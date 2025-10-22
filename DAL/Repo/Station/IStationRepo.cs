using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Station
{
    public interface IStationRepo
    {
        Task<IEnumerable<Entities.Station>> GetAllStationsAsync();
        Task<Entities.Station?> GetStationByIdAsync(int id);
        Task<Entities.Station?> GetStationByCodeAsync(string stationCode);
        Task<IEnumerable<Entities.Station>> GetStationsByNameAsync(string name);
        Task<string> AddTrainAsync(Entities.Station station);
        Task<string> UpdateStation(Entities.Station station);
        Task<string> DeleteStationByIdAsync(int id);
    }
}
