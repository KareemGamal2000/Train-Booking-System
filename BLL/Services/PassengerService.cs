using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Repository.Passenger;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepo _repo;
        private readonly IMapper _mapper;

        public PassengerService(IPassengerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PassengerDto>> GetAllAsync()
        {
            var passengers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PassengerDto>>(passengers);
        }

        public async Task<PassengerDto?> GetByIdAsync(int id)
        {
            var passenger = await _repo.GetByIdAsync(id);
            return _mapper.Map<PassengerDto>(passenger);
        }

        public async Task<string> AddAsync(CreatePassengerDto dto)
        {
            var passenger = _mapper.Map<Passenger>(dto);
            return await _repo.AddAsync(passenger);
        }

        public async Task<string> UpdateAsync(UpdatePassengerDto dto)
        {
            var passenger = _mapper.Map<Passenger>(dto);
            return await _repo.UpdateAsync(passenger);
        }

        public async Task<string> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}