using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos;
using Domain.Interfaces;
using Data.Entities;
using Data.Repository.Train;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TrainService : ITrainService
    {
        private readonly ITrainRepo _trainRepo;
        private readonly IMapper _mapper;

        public TrainService(ITrainRepo trainRepo, IMapper mapper)
        {
            _trainRepo = trainRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainDto>> GetAllAsync()
        {
            var trains = await _trainRepo.GetAllTrainsAsync();
            return _mapper.Map<IEnumerable<TrainDto>>(trains);
        }

        public async Task<TrainDto?> GetByIdAsync(Guid id)
        {
            var train = await _trainRepo.GetTrainByIdAsync(id);
            return _mapper.Map<TrainDto>(train);
        }

        public async Task<string> AddAsync(TrainDto train)
        {
            var entity = _mapper.Map<Train>(train);
            return await _trainRepo.AddTrainAsync(entity);
        }

        public async Task<string> UpdateAsync(TrainDto train)
        {
            var entity = _mapper.Map<Train>(train);
            return await _trainRepo.UpdateTrainAsync(entity);
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            return await _trainRepo.DeleteTrainAsync(id);
        }
    }
}
