using AutoMapper;
using Domain.Dtos;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Domain.Dtos;
using Domain.Dtos.TrainDtos;
using Domain.Dtos.StationDtos;

namespace Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Train, TrainReadDto>().ReverseMap();
            CreateMap<Coach, CoachDto>().ReverseMap();
            CreateMap<Station, StationReadDto>().ReverseMap();
        }
    }
}
