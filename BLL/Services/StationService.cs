using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Data.Models;
using Data.Repository.Station;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;

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

        public async Task<IEnumerable<StationDto>> GetAllStationAsync()
        {
            var stations = await _stationRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<StationDto>>(stations);
        }

        public async Task<StationDto?> GetStationByIdAsync(long id)
        {
            var station = await _stationRepo.GetByIdAsync(id);
            return _mapper.Map<StationDto>(station);
        }

        public async Task<string> AddStationAsync(StationDto stationDto)
        {
            var entity = _mapper.Map<Station>(stationDto);
            await _stationRepo.AddAsync(entity);
            return "Station added successfully";
        }

        public async Task<string> UpdateStationAsync(StationDto stationDto)
        {
            var entity = _mapper.Map<Station>(stationDto);
            _stationRepo.Update(entity);
            return "Station updated successfully";
        }

        public async Task<string> DeleteStationAsync(long id)
        {
            var station = await _stationRepo.GetByIdAsync(id);
            if (station == null)
            {
                return "Station not found";
            }
            _stationRepo.Delete(station);
            return "Station deleted successfully";
        }
    }
}
