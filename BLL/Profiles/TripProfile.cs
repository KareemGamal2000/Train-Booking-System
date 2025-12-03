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

        public static TripReadDto ToTripReadDto(this Trip trip)
        {
            if (trip == null)
            {
                return null;
            }

            var availableClasses = trip.SegmentPrices?
                .Where(sp => sp.Class != null)
                .Select(sp => new TripClassDto
                {
                    ClassID = sp.Class.Class_ID,
                    ClassNameAR = sp.Class.ClassNameAR
                })
                .Where(c => !string.IsNullOrEmpty(c.ClassNameAR))
                .GroupBy(c => c.ClassID)
                .Select(g => g.First())
                .ToList() ?? new List<TripClassDto>();

            return new TripReadDto
            {
                Trip_ID = trip.TripID,

                TrainID = trip.TrainID,
                TrainName = trip.Train?.TrainName ?? "غير محدد",
                AvailableClasses = availableClasses ?? new List<TripClassDto>(),
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
        public static TripReadDto ToTripReadDto(this Trip trip, string departureStationName, string arrivalStationName)
        {
            if (trip == null)
            {
                return null;
            }

            // 1. حساب الدرجات المتاحة
            var availableClasses = trip.SegmentPrices?
                .Where(sp => sp.Class != null)
                .Select(sp => new TripClassDto
                {
                    ClassID = sp.Class.Class_ID,
                    ClassNameAR = sp.Class.ClassNameAR
                })
                .Where(c => !string.IsNullOrEmpty(c.ClassNameAR))
                .GroupBy(c => c.ClassID)
                .Select(g => g.First())
                .ToList() ?? new List<TripClassDto>();

            var lowerDeparture = departureStationName?.ToLower();
            var lowerArrival = arrivalStationName?.ToLower();

            var startStopEntity = trip.Stops
                .OrderBy(ts => ts.StopSequence)
                .FirstOrDefault(ts =>
                    (ts.Station?.StationNameAR.ToLower().StartsWith(lowerDeparture) ?? false) ||
                    (ts.Station?.StationNameEN.ToLower().StartsWith(lowerDeparture) ?? false)
                );

            var endStopEntity = trip.Stops
                .OrderByDescending(ts => ts.StopSequence) 
                .FirstOrDefault(ts =>
                    (ts.Station?.StationNameAR.ToLower().StartsWith(lowerArrival) ?? false) ||
                    (ts.Station?.StationNameEN.ToLower().StartsWith(lowerArrival) ?? false)
                );

            // التأكد من أن محطة المغادرة تأتي قبل الوصول (شرط أساسي لرحلة صحيحة)
            if (startStopEntity != null && endStopEntity != null && startStopEntity.StopSequence > endStopEntity.StopSequence)
            {
                // إذا كان التسلسل خطأ، نجعل النهاية "آخر" تطابق بعد البداية
                endStopEntity = trip.Stops
                    .OrderBy(ts => ts.StopSequence)
                    .LastOrDefault(ts =>
                        ((ts.Station?.StationNameAR.ToLower().StartsWith(lowerArrival) ?? false) ||
                         (ts.Station?.StationNameEN.ToLower().StartsWith(lowerArrival) ?? false)) &&
                         ts.StopSequence > startStopEntity.StopSequence // يجب أن يكون التسلسل بعد المغادرة
                    );
            }


            // استخدام تسلسل التوقف (StopSequence) لضمان الفلترة
            int? startSequence = startStopEntity?.StopSequence;
            int? endSequence = endStopEntity?.StopSequence;


            var filteredStops = new List<TripStopDto>();

            if (startSequence.HasValue && endSequence.HasValue && startSequence.Value <= endSequence.Value)
            {
                filteredStops = trip.Stops?
                    .Where(ts => ts.StopSequence >= startSequence.Value && ts.StopSequence <= endSequence.Value)
                    .Select(s => s.ToTripStopDto())
                    .OrderBy(s => s.StopSequence)
                    .ToList() ?? new List<TripStopDto>();
            }
            else
            {
                filteredStops = new List<TripStopDto>();
            }

            var filteredSegmentPrices = new List<TripSegmentPriceDto>();

            var departureStopId = startStopEntity?.TripStopID;
            var arrivalStopId = endStopEntity?.TripStopID;


            if (departureStopId.HasValue && arrivalStopId.HasValue)
            {
                filteredSegmentPrices = trip.SegmentPrices?
                   .Where(p => p.StartStopID == departureStopId.Value && p.EndStopID == arrivalStopId.Value)
                   .Select(p => p.ToTripSegmentPriceDto())
                   .ToList() ?? new List<TripSegmentPriceDto>();
            }
            else
            {
                filteredSegmentPrices = new List<TripSegmentPriceDto>();
            }


            return new TripReadDto
            {
                Trip_ID = trip.TripID,

                TrainID = trip.TrainID,
                TrainName = trip.Train?.TrainName ?? "غير محدد",
                AvailableClasses = availableClasses ?? new List<TripClassDto>(),

                // نستخدم هنا بيانات المحطة الفعلية التي وجدناها (إذا كانت موجودة) أو الأسماء المدخلة
                DepartureStationID = startStopEntity?.StationID ?? trip.DepartureStationID.GetValueOrDefault(),
                DepartureStationNameAR = startStopEntity?.Station?.StationNameAR ?? departureStationName,
                ArrivalStationID = endStopEntity?.StationID ?? trip.ArrivalStationID.GetValueOrDefault(),
                ArrivalStationNameAR = endStopEntity?.Station?.StationNameAR ?? arrivalStationName,

                // ربط محطات التوقف: نستخدم القائمة المفلترة فقط
                Stops = filteredStops,

                // ربط الأسعار: نستخدم القائمة المفلترة فقط
                SegmentPrices = filteredSegmentPrices
            };
        }
    }
}
