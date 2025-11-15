using Data.Models;
using Domain.Dtos.StationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Profiles
{
    public static class StationProfile
    {
        public static StationReadDto ToStationReadDto(this Station station)
        {
            if (station == null) return null;
            return new StationReadDto
            {
                StationID = station.StationID,
                StationNameAR = station.StationNameAR,
                StationNameEN = station.StationNameEN,
                StationCode = station.StationCode,
                ShortName = station.ShortName,
                IsActive = station.IsActive
            };
        }
        public static Station ToStationModel(this StationCreateDto stationCreateDto)
        {
            if (stationCreateDto == null) return null;
            return new Station
            {
                StationID = stationCreateDto.StationID,
                StationNameAR = stationCreateDto.StationNameAR,
                StationNameEN = stationCreateDto.StationNameEN,
                StationCode = stationCreateDto.StationCode,
                ShortName = stationCreateDto.ShortName,
                IsActive = stationCreateDto.IsActive
            };
        }
    }
}
