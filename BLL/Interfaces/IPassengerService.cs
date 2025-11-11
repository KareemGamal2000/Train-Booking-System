using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;

namespace Domain.Interfaces
{
    public interface IPassengerService
    {
        Task<IEnumerable<PassengerDto>> GetAllAsync();
        Task<PassengerDto?> GetByIdAsync(int id);
        Task<string> AddAsync(CreatePassengerDto passengerDto);
        Task<string> UpdateAsync(UpdatePassengerDto passengerDto);
        Task<string> DeleteAsync(int id);
    }
}
