using Data.Models.Trips;
using Domain.Dtos.TripDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Profiles
{
    public static class TripProfile
    {
        public static Trip ToTripModel(this TripCreateDto dto)
        {
            if (dto == null) return null;
            return new Trip
            {
                TrainID = dto.TrainID,
                DepartureStationID = dto.DepartureStationID,
                ArrivalStationID = dto.ArrivalStationID
            };
        }

        public static TripStopDto ToTripStopDto(this TripStop stop)
        {
            if (stop == null) return null;
            return new TripStopDto
            {
                TripStopID = stop.TripStopID,
                StationID = stop.StationID.GetValueOrDefault(),
                StationNameAR = stop.Station?.StationNameAR ?? "N/A",
                StopSequence = stop.StopSequence,
                ArrivalTime = stop.ArrivalTime,
                DepartureTime = stop.DepartureTime,
                DistanceFromStartKM = stop.DistanceFromStartKM
            };
        }
        public static TripSegmentPriceDto ToTripSegmentPriceDto(this TripSegmentPrice price)
        {
            if (price == null) return null;
            return new TripSegmentPriceDto
            {
                SegmentPriceID = price.SegmentPriceID,
                ClassID = price.ClassID,
                ClassNameAR = price.Class?.ClassNameAR ?? "N/A", 
                StartStopID = price.StartStopID,
                EndStopID = price.EndStopID,
                Price = price.Price
            };
        }

        public static TripReadDto ToTripReadDto(this Trip trip)
        {
            if (trip == null)
            {
                return null;
            }

            return new TripReadDto
            {
                Trip_ID = trip.TripID,

                TrainID = trip.TrainID,
                TrainName = trip.Train?.TrainName ?? "غير محدد",

                DepartureStationID = trip.DepartureStationID.GetValueOrDefault(),
                DepartureStationNameAR = trip.Departure_Station?.StationNameAR ?? "N/A",
                ArrivalStationID = trip.ArrivalStationID.GetValueOrDefault(),
                ArrivalStationNameAR = trip.Arrival_Station?.StationNameAR ?? "N/A",

                // ربط محطات التوقف
                Stops = trip.Stops?.Select(s => s.ToTripStopDto()).OrderBy(s => s.StopSequence).ToList() ?? new List<TripStopDto>(),

                // ربط الأسعار (يتطلب Include لـ SegmentPrices)
                SegmentPrices = trip.SegmentPrices?.Select(p => p.ToTripSegmentPriceDto()).ToList() ?? new List<TripSegmentPriceDto>()
            };
        }
    }
}
