using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Trips;


namespace Data.Repository.TripSegmentPrices
{
    public interface ITripSegmentPriceRepo
    {
        Task<TripSegmentPrice> GetById(int id);
        Task<List<TripSegmentPrice>> GetAll();
        Task Add(TripSegmentPrice entity);
        Task Update(TripSegmentPrice entity);
        Task Delete(int id);
 

    }
}
