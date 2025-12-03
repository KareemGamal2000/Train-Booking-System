using Data.Repository.MainRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Trip
{
    public interface ITripRepo : IGenericRepo<Data.Models.Trips.Trip>
    {
       Task<(IEnumerable<Data.Models.Trips.Trip> Trips, int TotalCount)> GetAllTripsWithDetailsAsync(int pageNumber, int pageSize);
       Task<Data.Models.Trips.Trip?> GetTripDetailsAsync(int tripId);

        Task<IEnumerable<Data.Models.Trips.Trip>> GetTripsByStationAsync(string stationName);

        Task<IEnumerable<Data.Models.Trips.Trip>> FindTripsWithTwoStationsAsync(string departureStationName, string arrivalStationName);
    }
}
