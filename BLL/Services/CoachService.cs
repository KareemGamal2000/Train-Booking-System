using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos;
using Domain.Interfaces;
using Data.Repository.Coach;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace Domain.Services
{
    public class CoachService : ICoachService
    {
        private readonly ICoachRepo _coachRepo;
        private readonly IMapper _mapper;

        public CoachService(ICoachRepo coachRepo, IMapper mapper)
        {
            _coachRepo = coachRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CoachDto>> GetAllAsync()
        {
            var coaches = await _coachRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CoachDto>>(coaches);
        }

        public async Task<CoachDto?> GetByIdAsync(long id)
        {
            var coach = await _coachRepo.GetCoachWithSeatsAndClassAsync(id);
            return _mapper.Map<CoachDto>(coach);
        }

        public async Task<string> AddAsync(CoachDto coach)
        {
            var entity = _mapper.Map<Coach>(coach);
            await _coachRepo.AddAsync(entity);
            return "Add successful";
        }

        public async Task<string> UpdateAsync(CoachDto coach)
        {
            var entity = _mapper.Map<Coach>(coach);
            _coachRepo.Update(entity); 
            return "Update successful";
        }

        
    }
}
