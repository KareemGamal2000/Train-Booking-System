using Data.Models;
using Domain.Dtos.TrainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Profiles
{
    public static class TrainProfile
    {
        public static TrainReadDto ToTrainReadDto(this Train train)
        {
            if (train == null)
            {
                return null;
            }

            var groupedClasses = train.TrainCoaches?
                .Where(tc => tc.Coach?.Class != null)
                .GroupBy(tc => tc.Coach.Class.Class_ID)
                .Select(g => new TrainWithClassesDto
                {
                    ClassID = g.Key,
                    ClassNameAR = g.FirstOrDefault().Coach.Class.ClassNameAR,
                    ClassNameEN = g.FirstOrDefault().Coach.Class.ClassNameEN,
                    NumberOfCoaches = g.Count(),
                    TotalAvailableSeats = g.Sum(tc => tc.AvailableSeats)
                })
                .ToList() ?? new List<TrainWithClassesDto>();

            return new TrainReadDto
            {
                Train_ID = train.TrainID,
                TrainName = train.TrainName,
                AvailableClasses = groupedClasses
            };
        }
        public static TrainCreateDto ToTrainCreateDto(this Train train)
        {
            if (train == null)
            {
                return null;
            }

            return new TrainCreateDto
            {
                Train_ID = train.TrainID,
                TrainName = train.TrainName
               
            };
        }
        public static Train ToTrainModel(this TrainCreateDto trainCreateDto)
        {
            if (trainCreateDto == null)
            {
                return null;
            }
            return new Train
            {
                TrainID = trainCreateDto.Train_ID,
                TrainName = trainCreateDto.TrainName
            };
        }
        
    }
}
