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

        public async Task<(IEnumerable<Data.Models.Trips.Trip> Trips, int TotalCount)> GetAllTripsWithDetailsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10; 

            var query = _dbSet.AsNoTracking();

            var totalCount = await query.CountAsync();

            var trips = await query
                .OrderBy(t => t.TripID) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsSplitQuery()
                .Include(t => t.Train)     
                .Include(t => t.Departure_Station)
                .Include(t => t.Arrival_Station)
                .Include(t => t.Stops.OrderBy(ts => ts.StopSequence)) 
                    .ThenInclude(ts => ts.Station)
                .Include(t => t.SegmentPrices) // أسعار مقاطع الرحلة
                    .ThenInclude(p => p.Class) // بيانات الدرجة لكل سعر
                .ToListAsync();

            return (trips, totalCount);
        }
        public async Task<Data.Models.Trips.Trip?> GetTripDetailsAsync(int tripId)
        {
            return await GetFirstOrDefaultAsync(filter: t => t.TripID == tripId, include: new string[]
                {
                    "Train",
                    "Departure_Station",
                    "Arrival_Station",
                    "Stops.Station",
                    "SegmentPrices.Class"
                });

        }

        public async Task<IEnumerable<Data.Models.Trips.Trip>> GetTripsByStationAsync(string stationName)
        {

            var tripIdsQuery = _context.Trips.AsNoTracking()
                .Where(t => t.Stops.Any(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(stationName.ToLower()) ||
                    ts.Station.StationNameEN.ToLower().Contains(stationName.ToLower()) ||
                    ts.Station.ShortName.ToLower().Contains(stationName.ToLower())
                ))
                .Select(t => t.TripID) 
                .Distinct();

            var tripIds = await tripIdsQuery.ToListAsync();

            if (!tripIds.Any())
            {
                return Enumerable.Empty<Data.Models.Trips.Trip>();
            }
            string[] includes = new string[]
              {
                   "Train.TrainCoaches.Coach.Class",
                   "Departure_Station",
                   "Arrival_Station",
                   "Stops.Station",
                   "SegmentPrices.Class",
              };

            var finalTrips = await GetAllAsync(
                filter: t => tripIds.Contains(t.TripID),
                include: includes
            );

            foreach (var trip in finalTrips)
            {
                if (trip.Stops != null)
                {
                    trip.Stops = trip.Stops.OrderBy(ts => ts.StopSequence).ToList();
                }
            }
            return finalTrips;

        }

        public async Task<IEnumerable<Data.Models.Trips.Trip>> FindTripsWithTwoStationsAsync(string departureStationName, string arrivalStationName)
        {

            var lowerDeparture = departureStationName.ToLower();
            var lowerArrival = arrivalStationName.ToLower();

            var tripIdsQuery = _context.Trips.AsNoTracking()
                .Where(t => t.Stops.Any(tsDep =>
                    (tsDep.Station.StationNameAR.ToLower().StartsWith(lowerDeparture))
                    &&
                    t.Stops.Any(tsArr =>
                        (tsArr.Station.StationNameAR.ToLower().StartsWith(lowerArrival))
                    && tsDep.StopSequence < tsArr.StopSequence
                    )
                ))
                .Select(t => t.TripID)
                .Distinct();

            var tripIds = await tripIdsQuery.ToListAsync();
            if (!tripIds.Any())
            {
                return Enumerable.Empty<Data.Models.Trips.Trip>();
            }
            string[] includes = new string[]
               {
                   "Train.TrainCoaches.Coach",
                   "Train.TrainCoaches.Coach.Class",
                   "Departure_Station",
                   "Arrival_Station",
                   "Stops.Station",
                   "SegmentPrices.Class",
                   "SegmentPrices.StartStop.Station", 
                   "SegmentPrices.EndStop.Station"   
               };
              
            var finalTrips = await GetAllAsync(
                filter: t => tripIds.Contains(t.TripID),
                include: includes
            );

            foreach (var trip in finalTrips)
            {
                if (trip.Stops != null)
                {
                    trip.Stops = trip.Stops.OrderBy(ts => ts.StopSequence).ToList();
                }
            }
            return finalTrips;
        }

        public async Task<IEnumerable<Data.Models.Trips.Trip>> FindTripsWithTwoStationIdsAsync(
            long departureStationId,
            long arrivalStationId)
        {
            if (departureStationId <= 0 || arrivalStationId <= 0)
            {
                Console.WriteLine($"⚠️ معرفات غير صحيحة: Dep={departureStationId}, Arr={arrivalStationId}");
                return Enumerable.Empty<Data.Models.Trips.Trip>();
            }

            Console.WriteLine($"🔍 البحث عن رحلات من محطة {departureStationId} إلى محطة {arrivalStationId}");

            try
            {
                var depExists = await _context.Stations.AnyAsync(s => s.StationID == departureStationId);
                var arrExists = await _context.Stations.AnyAsync(s => s.StationID == arrivalStationId);
                
                Console.WriteLine($"📊 المحطة الأولى موجودة: {depExists}, المحطة الثانية موجودة: {arrExists}");

                if (!depExists || !arrExists)
                {
                    Console.WriteLine("❌ إحدى المحطات غير موجودة في قاعدة البيانات!");
                    return Enumerable.Empty<Data.Models.Trips.Trip>();
                }

                var tripIdsQuery = _dbSet
                    .AsNoTracking()
                    .Where(t => t.Stops.Any(tsDep =>
                        tsDep.StationID.HasValue && 
                        tsDep.StationID.Value == departureStationId &&
                        t.Stops.Any(tsArr =>
                            tsArr.StationID.HasValue &&
                            tsArr.StationID.Value == arrivalStationId &&
                            tsDep.StopSequence < tsArr.StopSequence
                        )
                    ))
                    .Select(t => t.TripID)
                    .Distinct();

                var tripIds = await tripIdsQuery.ToListAsync();

                Console.WriteLine($"📊 عدد معرفات الرحلات المطابقة: {tripIds.Count}");

                if (!tripIds.Any())
                {
                    Console.WriteLine("⚠️ لم يتم العثور على رحلات مطابقة");
                    return Enumerable.Empty<Data.Models.Trips.Trip>();
                }

                string[] includes = new string[]
                {
                    "Train.TrainCoaches.Coach",
                    "Train.TrainCoaches.Coach.Class",
                    "Departure_Station",
                    "Arrival_Station",
                    "Stops.Station",
                    "SegmentPrices.Class",
                    "SegmentPrices.StartStop.Station",
                    "SegmentPrices.EndStop.Station"
                };

                var finalTrips = await GetAllAsync(
                    filter: t => tripIds.Contains(t.TripID),
                    include: includes
                );

                foreach (var trip in finalTrips)
                {
                    if (trip.Stops != null)
                    {
                        trip.Stops = trip.Stops.OrderBy(ts => ts.StopSequence).ToList();
                    }
                }

                Console.WriteLine($"✅ تم العثور على {finalTrips.Count()} رحلة");
                return finalTrips;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطأ في البحث: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}