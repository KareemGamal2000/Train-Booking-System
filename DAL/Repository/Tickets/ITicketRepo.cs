using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Tickets;

namespace Data.Repository.Tickets
{
    public interface ITicketRepo
    {
        Task<Ticket> GetById(Guid id);
        Task<List<Ticket>> GetAll();
        Task Add(Ticket entity);
        Task Update(Ticket entity);
        Task Delete(Guid id);


    }
}
