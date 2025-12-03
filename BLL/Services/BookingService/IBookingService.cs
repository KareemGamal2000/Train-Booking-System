using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.BookingService
{
    public interface IBookingService
    {
        Task<BookingConfirmationDto> CreateBookingAsync(Guid userId, BookingCreateDto dto);
        Task<bool> CancelBookingAsync(BookingCancelDto dto);
        Task<BookingReadDto> GetBookingByIdAsync(Guid bookingId);
        Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(Guid userId);
        Task<bool> SelectSeatsAsync(Guid bookingId, BookingSeatSelectionDto dto);
        Task<BookingSummaryDto> GetBookingSummaryAsync(Guid bookingId);
        Task<bool> ConfirmBookingAsync(Guid bookingId); 
        Task<AvailableSeatsDto> GetAvailableSeatsAsync(int tripId, long classId, int departureStopId, int arrivalStopId);
    }
}
