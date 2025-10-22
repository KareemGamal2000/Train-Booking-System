using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Station
{
    public class StationRepo : IStationRepo
    {
        private readonly ApplicationDbContext _context;

        public StationRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Entities.Station>> GetAllStationsAsync()
        {
            return await _context.Stations.ToListAsync();
        }

        public async Task<Entities.Station?> GetStationByIdAsync(int id)
        {
            return await _context.Stations.FirstOrDefaultAsync(s => s.Station_ID == id);
        }

        public async Task<Entities.Station?> GetStationByCodeAsync(string stationCode)
        {
            return await _context.Stations.FirstOrDefaultAsync(s => s.StationCode == stationCode);
        }

        public async Task<IEnumerable<Entities.Station>> GetStationsByNameAsync(string name)
        {
            string normalizedName = name.ToLower();

            return await _context.Stations
                .Where(s => s.StationNameEN.ToLower().Contains(normalizedName) ||
                            s.StationNameAR.Contains(normalizedName))
                .ToListAsync();
        }
        public async Task<string> AddTrainAsync(Entities.Station station)
        {
            await _context.Stations.AddAsync(station);
            await _context.SaveChangesAsync();
            return "Station added successfully";
        }

        public async Task<string> UpdateStation(Entities.Station station)
        {
            _context.Stations.Update(station);
            await _context.SaveChangesAsync();
            return "Station updated successfully";
        }

        public async Task<string> DeleteStationByIdAsync(int id)
        {
            var station = await _context.Stations.FirstOrDefaultAsync(s => s.Station_ID == id);
            if (station != null)
            {
                _context.Stations.Remove(station);
                await _context.SaveChangesAsync();
                return "Station deleted successfully";
            }
            return "Station not found";

        }
    }
}
