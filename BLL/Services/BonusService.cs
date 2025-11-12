using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Services
{
    public class BonusService : IBonusService
    {
        public Task<BonusDto> GetBonusByPassengerIdAsync(Guid passengerId)
        {
            // هنا من المفروض نجيب بيانات البونص من الداتا
            var bonus = new BonusDto
            {
                PassengerId = passengerId,
                Points = 20,
                LastUpdated = DateTime.Now
            };
            return Task.FromResult(bonus);
        }

        public Task<bool> UpdateBonusAsync(BonusDto bonus)
        {
            // تحديث النقاط (في الحالة الحقيقية هنحدث في الـ DB)
            Console.WriteLine($"Updated bonus for passenger {bonus.PassengerId}: {bonus.Points} points");
            return Task.FromResult(true);
        }

        public Task<bool> RedeemBonusAsync(Guid passengerId, int pointsToRedeem)
        {
            // استبدال النقاط
            Console.WriteLine($"Passenger {passengerId} redeemed {pointsToRedeem} points");
            return Task.FromResult(true);
        }
    }
}
