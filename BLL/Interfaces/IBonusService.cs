using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;

namespace Domain.Interfaces
{
    public interface IBonusService
    {
        /// Get the bonus/discount info for a specific passenger.
        Task<BonusDto> GetBonusByPassengerIdAsync(Guid passengerId);

        /// Add or update bonus points for a passenger.
        Task<bool> UpdateBonusAsync(BonusDto bonus);

        /// Redeem points for discounts on a future trip.
        Task<bool> RedeemBonusAsync(Guid passengerId, int pointsToRedeem);
    }
}
