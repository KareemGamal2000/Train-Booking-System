using Data.Context;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Trip.TripStop
{
    public class TripStopRepo : GenericRepo<Data.Models.Trips.TripStop>, ITripStopRepo
    {
        public TripStopRepo(ApplicationDbContext context) : base(context) { }

       
        public async Task<IEnumerable<Data.Models.Trips.TripStop>> GetTripStopsByTripIdAsync(int tripId)
        {
            return await GetAllWithOrderingAsync(filter: ts => ts.TripID == tripId,include: new string[] { "Station" },orderBy: q => q.OrderBy(ts => ts.StopSequence));  
          
        }

    }
}
