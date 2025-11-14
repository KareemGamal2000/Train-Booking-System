using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;




namespace Data.Repository.Passengers
{
    public interface IPassengerRepo 
    {
        Task<IEnumerable<Passenger>> GetAllAsync();
        Task<Passenger?> GetByIdAsync(int id);
        Task<string> AddAsync(Passenger passenger);
        Task<string> UpdateAsync(Passenger passenger);
        Task<string> DeleteAsync(int id);
    }
}