using Domain.Dtos.TripDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TripService
{
    public interface ITripService
    {
        Task<IEnumerable<TripReadDto>> GetAllTripsAsync();
        Task<TripReadDto?> GetTripByIdAsync(int tripId);
        Task<TripReadDto?> GetTripDetailsAsync(int tripId);
        Task<IEnumerable<TripReadDto>> GetTripsByStationAsync(string stationName);
        Task<IEnumerable<TripReadDto>> FindTripsWithTwoStationsAsync(string departureStationName, string arrivalStationName);
        Task<TripReadDto> CreateTripAsync(TripCreateDto tripDto);
        Task<bool> UpdateTripAsync(int tripId, TripCreateDto tripDto);
        Task<bool> DeleteTripAsync(int tripId);
    }
}
