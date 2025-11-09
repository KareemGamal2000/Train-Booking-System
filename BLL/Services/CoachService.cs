using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Repo.Coach;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
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
            var coaches = await _coachRepo.GetCoachesAsync();
            return _mapper.Map<IEnumerable<CoachDto>>(coaches);
        }

        public async Task<CoachDto?> GetByIdAsync(Guid id)
        {
            var coach = await _coachRepo.GetCoachByIdAsync(id);
            return _mapper.Map<CoachDto>(coach);
        }

        public async Task<IEnumerable<CoachDto>> GetByTrainIdAsync(Guid trainId)
        {
            var coaches = await _coachRepo.GetCoachesByTrainIdAsync(trainId);
            return _mapper.Map<IEnumerable<CoachDto>>(coaches);
        }

        public async Task<string> AddAsync(CoachDto coach)
        {
            var entity = _mapper.Map<Coach>(coach);
            return await _coachRepo.AddCoachAsync(entity);
        }

        public async Task<string> UpdateAsync(CoachDto coach)
        {
            var entity = _mapper.Map<Coach>(coach);
            return await _coachRepo.UpdateCoachAsync(entity);
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            return await _coachRepo.DeleteCoachAsync(id);
        }
    }
}
