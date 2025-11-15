using Data.Repository.MainRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Station
{
    public interface IStationRepo : IGenericRepo<Data.Models.Station>
    {
        Task<IEnumerable<Data.Models.Station>> GetActiveStationsAsync();

        Task<Data.Models.Station?> GetStationBynameAsync(string stationname);
    }
}
