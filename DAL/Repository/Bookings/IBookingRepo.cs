using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repository.Bookings
{
    public interface IBookingRepo
    {
        Task<Booking> GetById(Guid id);
        Task<List<Booking>> GetAll();
        Task Add(Booking entity);
        Task Update(Booking entity);
        Task Delete(Guid id);

    }
}
