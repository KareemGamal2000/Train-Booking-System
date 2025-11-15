using Data.Context;
using Data.Models;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Station
{
    public class StationRepo : GenericRepo<Data.Models.Station> , IStationRepo
    {
        public StationRepo(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Data.Models.Station>> GetActiveStationsAsync()
        {
            return await _dbSet.AsNoTracking()
                               .Where(s => s.IsActive)
                               .ToListAsync();
        }

        public async Task<Data.Models.Station?> GetStationBynameAsync(string stationname)
        {
            var station = await _dbSet
                .Where(s => s.StationNameAR.Contains(stationname) || s.StationNameEN.Contains(stationname))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return station;
        }

    }
}
