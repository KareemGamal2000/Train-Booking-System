using Data.Repository.UnitOfWork;
using Domain.Dtos.Pagination;
using Domain.Dtos.TripDtos;
using Domain.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TripService
{
    public class TripService: ITripService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TripService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<PageResultDto<TripReadDto>> GetAllTripsAsync(int pageNumber, int pageSize)
        {
            var (trips, totalCount) = await _unitOfWork.Trip.GetAllTripsWithDetailsAsync(pageNumber, pageSize);

            var tripDtos = trips.Select(t => t.ToTripReadDto()).ToList();

            return new PageResultDto<TripReadDto>
            {
                Items = tripDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<TripReadDto?> GetTripByIdAsync(int tripId)
        {
            var trip = await _unitOfWork.Trip.GetByIdAsync(tripId);
            return trip?.ToTripReadDto();
        }

        public async Task<TripReadDto?> GetTripDetailsAsync(int tripId)
        {
            var trip = await _unitOfWork.Trip.GetTripDetailsAsync(tripId);
            return trip?.ToTripReadDto();
        }

        public async Task<IEnumerable<TripReadDto>> GetTripsByStationAsync(string stationName)
        {
            var trips = await _unitOfWork.Trip.GetTripsByStationAsync(stationName);
            return trips.Select(t => t.ToTripReadDto()).ToList();
        }

        public async Task<IEnumerable<TripReadDto>> FindTripsWithTwoStationsAsync(string departureStationName, string arrivalStationName)
        {
            var trips = await _unitOfWork.Trip.FindTripsWithTwoStationsAsync(departureStationName, arrivalStationName);
            return trips.Select(t => t.ToTripReadDto(departureStationName, arrivalStationName)).ToList();
        }
        public async Task<IEnumerable<TripReadDto>> FindTripsWithTwoStationsIdAsync(long departureStationId, long arrivalStationId)
        {
            var trips = await _unitOfWork.Trip.FindTripsWithTwoStationIdsAsync(departureStationId, arrivalStationId);
            return trips.Select(t => t.ToTripReadDto(departureStationId.ToString(), arrivalStationId.ToString())).ToList();
        }

        public async Task<TripReadDto> CreateTripAsync(TripCreateDto tripDto)
        {
            var tripModel = tripDto.ToTripModel();

            await _unitOfWork.Trip.AddAsync(tripModel);
            await _unitOfWork.SaveChangesAsync();

            return tripModel.ToTripReadDto();
        }

        public async Task<bool> UpdateTripAsync(int tripId, TripCreateDto tripDto)
        {
            var tripToUpdate = await _unitOfWork.Trip.GetByIdAsync(tripId);
            if (tripToUpdate == null) return false;

            // تحديث الخصائص الأساسية
            tripToUpdate.TrainID = tripDto.TrainID;
            tripToUpdate.DepartureStationID = tripDto.DepartureStationID;
            tripToUpdate.ArrivalStationID = tripDto.ArrivalStationID;

            _unitOfWork.Trip.Update(tripToUpdate);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteTripAsync(int tripId)
        {
            var tripToDelete = await _unitOfWork.Trip.GetByIdAsync(tripId);
            if (tripToDelete == null) return false;

            _unitOfWork.Trip.Delete(tripToDelete);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
