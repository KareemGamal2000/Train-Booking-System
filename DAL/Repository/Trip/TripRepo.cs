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
    public class TripRepo : GenericRepo<Data.Models.Trips.Trip>, ITripRepo
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

        public async Task<IEnumerable<Data.Models.Trips.Trip>> GetTripsByStationAsync(string stationName)
        {

            var tripIdsQuery = _context.Trips.AsNoTracking()
                .Where(t => t.Stops.Any(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(stationName.ToLower()) ||
                    ts.Station.StationNameEN.ToLower().Contains(stationName.ToLower()) ||
                    ts.Station.ShortName.ToLower().Contains(stationName.ToLower())
                ))
                .Select(t => t.TripID) // جلب الـ ID فقط!
                .Distinct();

            var tripIds = await tripIdsQuery.ToListAsync();

            if (!tripIds.Any())
            {
                return Enumerable.Empty<Data.Models.Trips.Trip>();
            }

            var tripsQuery = _dbSet.AsNoTracking()
                .Where(t => tripIds.Contains(t.TripID)) 
                .Include(t => t.Train)
                     .ThenInclude(tr => tr.TrainCoaches)
                          .ThenInclude(tc => tc.Coach)
                               .ThenInclude(c => c.Class)
                .Include(t => t.Departure_Station)
                .Include(t => t.Arrival_Station)
                .Include(t => t.Stops.OrderBy(ts => ts.StopSequence)) // ترتيب محطات التوقف
                    .ThenInclude(ts => ts.Station)
                .Include(t => t.SegmentPrices)
                    .ThenInclude(p => p.Class);

            var finalTrips = await tripsQuery.ToListAsync();

            return finalTrips;

        }

        public async Task<IEnumerable<Data.Models.Trips.Trip>> FindTripsWithTwoStationsAsync(string departureStationName, string arrivalStationName)
        {

            var tripIdsQuery = _context.Trips.AsNoTracking()
                .Where(t => t.Stops.Any(tsDep =>
                    (tsDep.Station.StationNameAR.ToLower().Contains(departureStationName.ToLower()) ||
                     tsDep.Station.StationNameEN.ToLower().Contains(departureStationName.ToLower()))
                    &&
                    t.Stops.Any(tsArr =>
                        (tsArr.Station.StationNameAR.ToLower().Contains(arrivalStationName.ToLower()) ||
                         tsArr.Station.StationNameEN.ToLower().Contains(arrivalStationName.ToLower()) )
                    && tsDep.StopSequence < tsArr.StopSequence
                    )
                ))
                .Select(t => t.TripID) // جلب الـ ID فقط!
                .Distinct();

            var tripIds = await tripIdsQuery.ToListAsync();
            if (!tripIds.Any())
            {
                return Enumerable.Empty<Data.Models.Trips.Trip>();
            }

            var tripsQuery = _dbSet.AsNoTracking()
                .Where(t => tripIds.Contains(t.TripID)) // فلترة باستخدام الـ IDs المفلترة
                .Include(t => t.Train)
                     .ThenInclude(tr => tr.TrainCoaches)
                         .ThenInclude(tc => tc.Coach)
                          .ThenInclude(c => c.Class)
                .Include(t => t.Departure_Station)
                .Include(t => t.Arrival_Station)
                .Include(t => t.Stops.OrderBy(ts => ts.StopSequence))
                    .ThenInclude(ts => ts.Station)
                .Include(t => t.SegmentPrices)
                    .ThenInclude(p => p.Class);

            var finalTrips = await tripsQuery.ToListAsync();

            return finalTrips;

        }


    }
}