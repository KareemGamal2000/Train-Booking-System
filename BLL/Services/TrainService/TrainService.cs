using AutoMapper;
using Data.Models;
using Data.Repository.Train;
using Data.Repository.UnitOfWork;
using Domain.Dtos;
using Domain.Dtos.TrainDtos;
using Domain.Interfaces;
using Domain.Profiles;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace Domain.Services.TrainService
{
    public class TrainService : ITrainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<TrainReadDto>> GetAllTrainsAsync()
        {
            var includes = new string[] { "TrainCoaches.Coach.Class" };
            var trains = await _unitOfWork.Train.GetAllAsync(include: includes);
            return trains.Select(t => t.ToTrainReadDto()).ToList();
        }
        public async Task<IEnumerable<TrainReadDto>> GetAllTrainsWithClassesAsync()
        {
            var trains = await _unitOfWork.Train.GetAllTrainsWithClassesAsync();
            return trains.Select(t => t.ToTrainReadDto()).ToList();
        }

        public async Task<TrainReadDto?> GetTrainByIdAsync(long trainId)
        {
            var train = await _unitOfWork.Train.GetTrainWithClassesByIdAsync(trainId);

            return train?.ToTrainReadDto();
        }
        public async Task<TrainReadDto?> GetTrainByNameAsync(string trainName)
        {
            var train = await _unitOfWork.Train.GetTrainByTrainNameAsync(trainName);

            return train?.ToTrainReadDto();
        }
        public async Task<TrainCreateDto> CreateTrainAsync(TrainCreateDto trainDto)
        {
            var existingTrain = await _unitOfWork.Train.GetByIdAsync(trainDto.Train_ID);
            if (existingTrain != null)
            {
                throw new InvalidOperationException($"القطار برقم {trainDto.Train_ID} موجود بالفعل.");
            }

            var trainModel = trainDto.ToTrainModel();

            _unitOfWork.Train.AddAsync(trainModel);
            await _unitOfWork.SaveChangesAsync();
            return trainModel.ToTrainCreateDto();
        }

        public async Task<bool> UpdateTrainAsync(string trainName, TrainCreateDto trainDto)
        {
            var trainToUpdate = await _unitOfWork.Train.GetTrainByTrainNameAsync(trainName);
            if (trainToUpdate == null) return false;

            trainToUpdate.TrainName = trainDto.TrainName;

            _unitOfWork.Train.Update(trainToUpdate);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteTrainAsync(long trainId)
        {
            var trainToDelete = await _unitOfWork.Train.GetByIdAsync(trainId);
            if (trainToDelete == null) return false;

            _unitOfWork.Train.Delete(trainToDelete);
            return await _unitOfWork.SaveChangesAsync();
        }

    }
}
