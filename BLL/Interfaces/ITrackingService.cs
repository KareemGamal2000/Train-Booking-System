using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;

namespace Domain.Interfaces
{
    public interface ITrackingService
    {
        /// Get the real-time tracking data for a specific train.
        Task<TrainTrackingDto> GetTrainLocationAsync(Guid trainId);

        /// Update the live GPS coordinates and status of a train.
        /// (Used by assistant driver or system API)
        Task<bool> UpdateTrainLocationAsync(TrainTrackingDto trackingData);

        /// Get live status for all active trains.
        Task<IEnumerable<TrainTrackingDto>> GetAllActiveTrainsAsync();
    }
}
