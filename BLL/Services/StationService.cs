using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos;
using Domain.Interfaces;
using Data.Entities;
using Data.Repository.Station;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepo _stationRepo;
        private readonly IMapper _mapper;

        public StationService(IStationRepo stationRepo, IMapper mapper)
        {
            _stationRepo = stationRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StationDto>> GetAllAsync()
        {
            var stations = await _stationRepo.GetAllStationsAsync();
            return _mapper.Map<IEnumerable<StationDto>>(stations);
        }

        public async Task<StationDto?> GetByIdAsync(long id)
        {
            var station = await _stationRepo.GetStationByIdAsync(id);
            return _mapper.Map<StationDto>(station);
        }

        public async Task<string> AddStationAsync(StationDto stationDto)
        {
            var entity = _mapper.Map<Station>(stationDto);
            return await _stationRepo.AddStationAsync(entity);
        }

        public async Task<string> UpdateStationAsync(StationDto stationDto)
        {
            var entity = _mapper.Map<Station>(stationDto);
            return await _stationRepo.UpdateStationAsync(entity);
        }

        public async Task<string> DeleteStationAsync(long id)
        {
            return await _stationRepo.DeleteStationAsync(id);
        }
    }
}
