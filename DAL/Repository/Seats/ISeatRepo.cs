using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repository.Seats
{
    public interface ISeatRepo
    {
        Task<Seat> GetById(Guid id);
        Task<List<Seat>> GetAll();
        Task Add(Seat entity);
        Task Update(Seat entity);
        Task Delete(Guid id);
        Task<Seat> GetSeatByIdAsync(long seatId);
        Task MarkReservedAsync(long seatId);

    }
}
