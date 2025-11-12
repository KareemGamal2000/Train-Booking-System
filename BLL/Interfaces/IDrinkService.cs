using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDrinkService
    {
        Task<IEnumerable<DrinkDto>> GetAllDrinksAsync();
        Task<DrinkDto> GetDrinkByIdAsync(Guid id);
        Task<bool> OrderDrinksAsync(OrderDrinkDto order);
    }
}
