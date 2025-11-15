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
                .Where(t => t.Trip_ID == tripId)
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
            var stationNameLower = stationName.ToLower();

            return await _dbSet
                .AsNoTracking()
                .Where(t => t.Stops.Any(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(stationNameLower) || 
                    ts.Station.StationNameEN.ToLower().Contains(stationNameLower) || 
                    ts.Station.ShortName.ToLower().Contains(stationNameLower) 
                ))
                .Include(t => t.Departure_Station)
                .Include(t => t.Arrival_Station)
                .Include(t => t.Stops.OrderBy(ts => ts.StopSequence))
                    .ThenInclude(ts => ts.Station)
                .ToListAsync();
        }

        //  ميثود البحث عن رحلة بين محطتي مغادرة ووصول

        public async Task<IEnumerable<Data.Models.Trips.Trip>> FindTripsWithTwoStationsAsync(string departureStationName, string arrivalStationName)
        {
            var depNameLower = departureStationName.ToLower();
            var arrNameLower = arrivalStationName.ToLower();

            var candidateTrips = await _dbSet.AsNoTracking()
                .Where(t => t.Stops.Any(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(depNameLower) || ts.Station.StationNameEN.ToLower().Contains(depNameLower)
                ) && t.Stops.Any(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(arrNameLower) || ts.Station.StationNameEN.ToLower().Contains(arrNameLower)
                ))
                .Include(t => t.Departure_Station)
                .Include(t => t.Arrival_Station)
                .Include(t => t.Stops.OrderBy(ts => ts.StopSequence))
                    .ThenInclude(ts => ts.Station)
                .ToListAsync();
            var finalTrips = candidateTrips.Where(t =>
            {
                var departureStop = t.Stops.FirstOrDefault(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(depNameLower) || ts.Station.StationNameEN.ToLower().Contains(depNameLower));

                var arrivalStop = t.Stops.FirstOrDefault(ts =>
                    ts.Station.StationNameAR.ToLower().Contains(arrNameLower) || ts.Station.StationNameEN.ToLower().Contains(arrNameLower));

                // تأكيد وجود كلتا المحطتين وأن ترتيب المغادرة يأتي قبل الوصول
                return departureStop != null && arrivalStop != null && departureStop.StopSequence < arrivalStop.StopSequence;
            }).ToList();

            return finalTrips;

        }


    }
}
