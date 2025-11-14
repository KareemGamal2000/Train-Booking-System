using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Trips;

namespace Data.Repository.TripStops
{
    public interface ITripStopRepo
    {
        Task<TripStop> GetById(int id);
        Task<List<TripStop>> GetAll();
        Task Add(TripStop entity);
        Task Update(TripStop entity);
        Task Delete(int id);


    }
}
