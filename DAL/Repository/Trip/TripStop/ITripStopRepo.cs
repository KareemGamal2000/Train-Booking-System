using Data.Repository.MainRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Trip.TripStop
{
    public interface ITripStopRepo: IGenericRepo<Data.Models.Trips.TripStop>
    {
        Task<IEnumerable<Data.Models.Trips.TripStop>> GetTripStopsByTripIdAsync(int tripId);
    }
}
