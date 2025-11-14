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
        public Task<IEnumerable<DrinkDto>> GetAllDrinksAsync()
        {
            return Task.FromResult<IEnumerable<DrinkDto>>(_drinks);
        }
        public Task<DrinkDto> GetDrinkByIdAsync(Guid id)
        {
            var drink = _drinks.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(drink);
        }

        // البحث عن مشروب معيّن
        public Task<bool> OrderDrinksAsync(OrderDrinkDto order)
        {
            // لو مفيش مشروبات يبقى خطأ
            if (order.DrinkIds == null || !order.DrinkIds.Any())
                return Task.FromResult(false);

            // نجيب المشروبات المطلوبة
            var selectedDrinks = _drinks
                .Where(d => order.DrinkIds.Contains(d.Id) && d.IsAvailable)
                .ToList();

            if (!selectedDrinks.Any())
                return Task.FromResult(false);

            // حساب السعر
            order.TotalPrice = selectedDrinks.Sum(d => d.Price);

            Console.WriteLine($"Passenger {order.PassengerId} ordered {selectedDrinks.Count} drinks for seat {order.SeatNumber}.");

            return Task.FromResult(true);
        }
    }
}