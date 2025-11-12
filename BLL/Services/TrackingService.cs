using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly List<TrainTrackingDto> _trainTrackingData = new();

        public Task<IEnumerable<TrainTrackingDto>> GetAllActiveTrainsAsync()
        {
            var activeTrains = _trainTrackingData
                .Where(t => t.Status != "Arrived")
                .ToList();

            return Task.FromResult<IEnumerable<TrainTrackingDto>>(activeTrains);
        }

        public Task<TrainTrackingDto> GetTrainLocationAsync(Guid trainId)
        {
            var train = _trainTrackingData.FirstOrDefault(t => t.TrainId == trainId);
            return Task.FromResult(train);
        }

        public Task<bool> UpdateTrainLocationAsync(TrainTrackingDto trackingData)
        {
            var existingTrain = _trainTrackingData.FirstOrDefault(t => t.TrainId == trackingData.TrainId);

            if (existingTrain != null)
            {
                existingTrain.Latitude = trackingData.Latitude;
                existingTrain.Longitude = trackingData.Longitude;
                existingTrain.Status = trackingData.Status;
                existingTrain.ETA = trackingData.ETA;
                existingTrain.LastUpdated = DateTime.Now;
            }
            else
            {
                trackingData.LastUpdated = DateTime.Now;
                _trainTrackingData.Add(trackingData);
            }

            return Task.FromResult(true);
        }
    }
}