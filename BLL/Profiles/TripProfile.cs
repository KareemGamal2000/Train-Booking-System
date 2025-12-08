using Data.Models;
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
                StartStation = price.StartStop?.Station?.StationNameAR ?? "N/A",
                EndStation = price.EndStop?.Station?.StationNameAR ?? "N/A",
                Price = price.Price
            };
        }

        // ✅ الـ overload الأساسي - محسّن
        public static TripReadDto ToTripReadDto(this Trip trip)
        {
            if (trip == null) return null;

            // ✅ تحسين: حساب availableClasses مرة واحدة فقط
            var availableClasses = trip.SegmentPrices?
                .Where(sp => sp.Class != null && !string.IsNullOrEmpty(sp.Class.ClassNameAR))
                .GroupBy(sp => sp.Class.Class_ID)
                .Select(g => new TripClassDto
                {
                    ClassID = g.Key,
                    ClassNameAR = g.First().Class.ClassNameAR
                })
                .ToList() ?? new List<TripClassDto>();

            // ✅ تحسين: تحويل Stops مرة واحدة وترتيبها
            var stops = trip.Stops?
                .OrderBy(s => s.StopSequence)
                .Select(s => s.ToTripStopDto())
                .ToList() ?? new List<TripStopDto>();

            // ✅ تحسين: تحويل SegmentPrices مرة واحدة
            var segmentPrices = trip.SegmentPrices?
                .Select(p => p.ToTripSegmentPriceDto())
                .ToList() ?? new List<TripSegmentPriceDto>();

            return new TripReadDto
            {
                Trip_ID = trip.TripID,
                TrainID = trip.TrainID,
                TrainName = trip.Train?.TrainName ?? "غير محدد",
                AvailableClasses = availableClasses,
                DepartureStationID = trip.DepartureStationID.GetValueOrDefault(),
                DepartureStationNameAR = trip.Departure_Station?.StationNameAR ?? "N/A",
                ArrivalStationID = trip.ArrivalStationID.GetValueOrDefault(),
                ArrivalStationNameAR = trip.Arrival_Station?.StationNameAR ?? "N/A",
                Stops = stops,
                SegmentPrices = segmentPrices
            };
        }

        public static TripReadDto ToTripReadDto(this Trip trip, string departureStationName, string arrivalStationName)
        {
            if (trip == null) return null;

            var lowerDeparture = departureStationName?.ToLower() ?? "";
            var lowerArrival = arrivalStationName?.ToLower() ?? "";

            var orderedStops = trip.Stops?.OrderBy(ts => ts.StopSequence).ToList() ?? new List<TripStop>();

            TripStop startStopEntity = null;
            TripStop endStopEntity = null;

            foreach (var stop in orderedStops)
            {
                var stationNameAR = stop.Station?.StationNameAR?.ToLower() ?? "";
                var stationNameEN = stop.Station?.StationNameEN?.ToLower() ?? "";

                if (startStopEntity == null && 
                    (stationNameAR.StartsWith(lowerDeparture) || stationNameEN.StartsWith(lowerDeparture)))
                {
                    startStopEntity = stop;
                }
                if (startStopEntity != null && endStopEntity == null &&
                    (stationNameAR.StartsWith(lowerArrival) || stationNameEN.StartsWith(lowerArrival)))
                {
                    endStopEntity = stop;
                    break;
                }
            }

            var filteredStops = new List<TripStopDto>();
            var filteredSegmentPrices = new List<TripSegmentPriceDto>();

            if (startStopEntity != null && endStopEntity != null && 
                startStopEntity.StopSequence <= endStopEntity.StopSequence)
            {
                filteredStops = orderedStops
                    .Where(ts => ts.StopSequence >= startStopEntity.StopSequence && 
                                 ts.StopSequence <= endStopEntity.StopSequence)
                    .Select(s => s.ToTripStopDto())
                    .ToList();

                var departureStopId = startStopEntity.TripStopID;
                var arrivalStopId = endStopEntity.TripStopID;

                filteredSegmentPrices = trip.SegmentPrices?
                    .Where(p => p.StartStopID == departureStopId && p.EndStopID == arrivalStopId)
                    .Select(p => p.ToTripSegmentPriceDto())
                    .ToList() ?? new List<TripSegmentPriceDto>();
            }
            var availableClasses = trip.SegmentPrices?
                .Where(sp => sp.Class != null && !string.IsNullOrEmpty(sp.Class.ClassNameAR))
                .GroupBy(sp => sp.Class.Class_ID)
                .Select(g => new TripClassDto
                {
                    ClassID = g.Key,
                    ClassNameAR = g.First().Class.ClassNameAR
                })
                .ToList() ?? new List<TripClassDto>();

            return new TripReadDto
            {
                Trip_ID = trip.TripID,
                TrainID = trip.TrainID,
                TrainName = trip.Train?.TrainName ?? "غير محدد",
                AvailableClasses = availableClasses,
                DepartureStationID = startStopEntity?.StationID ?? trip.DepartureStationID.GetValueOrDefault(),
                DepartureStationNameAR = startStopEntity?.Station?.StationNameAR ?? departureStationName,
                ArrivalStationID = endStopEntity?.StationID ?? trip.ArrivalStationID.GetValueOrDefault(),
                ArrivalStationNameAR = endStopEntity?.Station?.StationNameAR ?? arrivalStationName,
                Stops = filteredStops,
                SegmentPrices = filteredSegmentPrices
            };
        }

        public static TripReadDto ToTripReadDtoByIds(this Trip trip, long departureStationId, long arrivalStationId)
        {
            if (trip == null) return null;

            var stopsDict = trip.Stops?
                .OrderBy(ts => ts.StopSequence)
                .ToDictionary(s => s.StationID ?? 0, s => s) 
                ?? new Dictionary<long, TripStop>();

            TripStop startStopEntity = stopsDict.GetValueOrDefault(departureStationId);
            TripStop endStopEntity = stopsDict.GetValueOrDefault(arrivalStationId);

            var filteredStops = new List<TripStopDto>();
            var filteredSegmentPrices = new List<TripSegmentPriceDto>();

            if (startStopEntity != null && endStopEntity != null && 
                startStopEntity.StopSequence <= endStopEntity.StopSequence)
            {
                filteredStops = stopsDict.Values
                    .Where(ts => ts.StopSequence >= startStopEntity.StopSequence && 
                                 ts.StopSequence <= endStopEntity.StopSequence)
                    .Select(s => s.ToTripStopDto())
                    .ToList();

                filteredSegmentPrices = trip.SegmentPrices?
                    .Where(p => p.StartStopID == startStopEntity.TripStopID && 
                                p.EndStopID == endStopEntity.TripStopID)
                    .Select(p => p.ToTripSegmentPriceDto())
                    .ToList() ?? new List<TripSegmentPriceDto>();
            }

            var availableClasses = trip.SegmentPrices?
                .Where(sp => sp.Class != null && !string.IsNullOrEmpty(sp.Class.ClassNameAR))
                .GroupBy(sp => sp.Class.Class_ID)
                .Select(g => new TripClassDto
                {
                    ClassID = g.Key,
                    ClassNameAR = g.First().Class.ClassNameAR
                })
                .ToList() ?? new List<TripClassDto>();

            return new TripReadDto
            {
                Trip_ID = trip.TripID,
                TrainID = trip.TrainID,
                TrainName = trip.Train?.TrainName ?? "غير محدد",
                AvailableClasses = availableClasses,
                DepartureStationID = startStopEntity?.StationID ?? departureStationId,
                DepartureStationNameAR = startStopEntity?.Station?.StationNameAR ?? "N/A",
                ArrivalStationID = endStopEntity?.StationID ?? arrivalStationId,
                ArrivalStationNameAR = endStopEntity?.Station?.StationNameAR ?? "N/A",
                Stops = filteredStops,
                SegmentPrices = filteredSegmentPrices
            };
        }
    }
}
