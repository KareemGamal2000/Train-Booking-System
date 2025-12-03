using Domain.Dtos.TrainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TrainService
{
    public interface ITrainService
    {
        Task<IEnumerable<TrainReadDto>> GetAllTrainsAsync();

        Task<IEnumerable<TrainReadDto>> GetAllTrainsWithClassesAsync();
        Task<TrainReadDto?> GetTrainByIdAsync(string trainId);

        Task<TrainReadDto?> GetTrainByNameAsync(string trainName);

        Task<TrainCreateDto> CreateTrainAsync(TrainCreateDto trainDto);

        Task<bool> UpdateTrainAsync(string trainName, TrainCreateDto trainDto);

        Task<bool> DeleteTrainAsync(long trainId);

    }
}
