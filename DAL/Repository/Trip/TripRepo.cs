using Data.Context;
using Data.Repository.MainRepo;
using Data.Repository.Station;
using Data.Models.Trips;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Trip
{
    public class TripRepo : GenericRepo<Data.Models.Trips.Trip> , ITripRepo
    {
        public TripRepo(ApplicationDbContext context) : base(context) { }

        public async Task<Data.Models.Trips.Trip?> GetTripDetailsAsync(int tripId)
        {
            return await _dbSet
                .Where(t => t.TripID == tripId)
                .Include(t => t.Train) // بيانات القطار
                .Include(t => t.Departure_Station) // محطة المغادرة الرئيسية
                .Include(t => t.Arrival_Station)   // محطة الوصول الرئيسية
                .Include(t => t.Stops.OrderBy(ts => ts.StopSequence)) // محطات التوقف بالترتيب
                    .ThenInclude(ts => ts.Station) // بيانات المحطة لكل توقف
                .Include(t => t.SegmentPrices) // أسعار مقاطع الرحلة
                .FirstOrDefaultAsync();
        }

        //ميثود جلب كل الرحلات التي تمر بمحطة معينة
        public async Task<IEnumerable<Data.Models.Trips.Trip>> GetTripsByStationAsync(string stationName)
        {
            var normalizedInput = NormalizeArabicName(stationName);

            var candidateTrips = await _dbSet
                .AsNoTracking()
                .Where(t => t.Stops.Any(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(stationName.ToLower()) ||
                    ts.Station.StationNameEN.ToLower().Contains(stationName.ToLower()) ||
                    ts.Station.ShortName.ToLower().Contains(stationName.ToLower())
                ))
                .Include(t => t.Train)
                .Include(t => t.Departure_Station)
                .Include(t => t.Arrival_Station)
                .Include(t => t.Stops)
                    .ThenInclude(ts => ts.Station)
                .ToListAsync();

            var finalTrips = candidateTrips.Where(t =>
                t.Stops.Any(ts =>
                    NormalizeArabicName(ts.Station.StationNameAR) == normalizedInput ||
                    NormalizeArabicName(ts.Station.StationNameEN) == normalizedInput ||
                    NormalizeArabicName(ts.Station.ShortName) == normalizedInput
                )
            ).ToList();

            return finalTrips;

        }
        public async Task<IEnumerable<Data.Models.Trips.Trip>> FindTripsWithTwoStationsAsync(string departureStationName, string arrivalStationName)
        {
            var normalizedDep = NormalizeArabicName(departureStationName);
            var normalizedArr = NormalizeArabicName(arrivalStationName);

            var candidateTrips = await _dbSet.AsNoTracking()
                .Where(t => t.Stops.Any(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(departureStationName.ToLower()) || ts.Station.StationNameEN.ToLower().Contains(departureStationName.ToLower())
                ) && t.Stops.Any(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(arrivalStationName.ToLower()) || ts.Station.StationNameEN.ToLower().Contains(arrivalStationName.ToLower())
                ))
                .Include(t => t.Train)
                .Include(t => t.Departure_Station)
                .Include(t => t.Arrival_Station)
                .Include(t => t.Stops.OrderBy(ts => ts.StopSequence))
                    .ThenInclude(ts => ts.Station)
                .ToListAsync(); // Fetch results to memory

            var finalTrips = candidateTrips.Where(t =>
            {
                bool IsFuzzyMatch(Data.Models.Station station, string normalizedName)
                {
                    return NormalizeArabicName(station.StationNameAR) == normalizedName ||
                           NormalizeArabicName(station.StationNameEN) == normalizedName ||
                           NormalizeArabicName(station.ShortName) == normalizedName;
                }

                var departureStop = t.Stops.FirstOrDefault(ts => IsFuzzyMatch(ts.Station, normalizedDep));

                var arrivalStop = t.Stops.FirstOrDefault(ts => IsFuzzyMatch(ts.Station, normalizedArr));

                return departureStop != null && arrivalStop != null && departureStop.StopSequence < arrivalStop.StopSequence;
            }).ToList();

            return finalTrips;

        }
        private static string NormalizeArabicName(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;

            name = name.Replace('أ', 'ا').Replace('إ', 'ا').Replace('آ', 'ا');

            name = name.Replace('ة', 'ه');

            return name.Trim().ToLower();
        }


    }
}
