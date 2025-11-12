using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly List<DrinkDto> _drinks = new()
        {
            new DrinkDto { Id = Guid.NewGuid(), Name = "Water", Price = 10, IsAvailable = true },
            new DrinkDto { Id = Guid.NewGuid(), Name = "Coffee", Price = 25, IsAvailable = true },
            new DrinkDto { Id = Guid.NewGuid(), Name = "Juice", Price = 20, IsAvailable = true }
        };

        // عرض كل المشروبات المتاحة
        public Task<IEnumerable<DrinkDto>> GetAllDrinksAsync()
        {
            return Task.FromResult<IEnumerable<DrinkDto>>(_drinks);
        }

        // البحث عن مشروب معيّن
        public Task<DrinkDto> GetDrinkByIdAsync(Guid id)
        {
            var drink = _drinks.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(drink);
        }

        // تنفيذ مشروب أثناء الرحلة
        public Task<bool> OrderDrinksAsync(OrderDrinkDto order)
        {
            var drink = _drinks.FirstOrDefault(d => d.Id == order.DrinkId);
            if (drink == null || !drink.IsAvailable)
                return Task.FromResult(false);

            Console.WriteLine($"Passenger {order.PassengerId} ordered {drink.Name} for seat {order.SeatNumber}.");
            return Task.FromResult(true);
        }
    }
}