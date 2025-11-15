using AutoMapper;
using Data.Models;
using Data.Repository.Station;
using Data.Repository.UnitOfWork;
using Domain.Dtos;
using Domain.Dtos.StationDtos;
using Domain.Profiles;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace Domain.Services.StationService
{

    public class StationService : IStationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StationReadDto>> GetAllStationAsync()
        {
            var stations = await _unitOfWork.Station.GetAllAsync();
            return stations.Select(s => s.ToStationReadDto()).ToList();
        }
        public async Task<StationReadDto?> GetStationByIdAsync(long stationId)
        {
            var station = await _unitOfWork.Station.GetByIdAsync(stationId);
            return station?.ToStationReadDto();
        }

        public async Task<StationReadDto?> GetStationBynameAsync(string stationname)
        {
            var station = await _unitOfWork.Station.GetStationBynameAsync(stationname);
            return station?.ToStationReadDto();
        }

        public async Task<StationReadDto> CreateStationAsync(StationCreateDto stationDto)
        {
            var existingStation = await _unitOfWork.Station.GetFirstOrDefaultAsync(s => s.StationNameAR == stationDto.StationNameAR);
            if (existingStation != null)
            {
                throw new InvalidOperationException($"المحطة بالاسم {stationDto.StationNameAR} موجودة بالفعل.");
            }

            var stationModel = stationDto.ToStationModel();

            await _unitOfWork.Station.AddAsync(stationModel);
            await _unitOfWork.SaveChangesAsync();

            return stationModel.ToStationReadDto();
        }

        public async Task<bool> UpdateStationAsync(string stationname, StationUpdateDto stationDto)
        {
            var stationToUpdate = await _unitOfWork.Station.GetStationBynameAsync(stationname);
            if (stationToUpdate == null) return false;

            stationToUpdate.StationNameAR = stationDto.StationNameAR;
            stationToUpdate.StationNameEN = stationDto.StationNameEN;
            stationToUpdate.StationCode = stationDto.StationCode;
            stationToUpdate.ShortName = stationDto.ShortName;
            stationToUpdate.IsActive = stationDto.IsActive;

            _unitOfWork.Station.Update(stationToUpdate);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteStationAsync(long stationId)
        {
            var stationToDelete = await _unitOfWork.Station.GetByIdAsync(stationId);
            if (stationToDelete == null) return false;

            _unitOfWork.Station.Delete(stationToDelete);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
