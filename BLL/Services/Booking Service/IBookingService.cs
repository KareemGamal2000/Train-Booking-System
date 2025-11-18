using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Booking_Service
{
    public interface IBookingService
    {
        Task<Guid> CreateBookingAsync(BookingCreateDto dto);
        Task<bool> CancelBookingAsync(BookingCancelDto dto);
        Task<BookingReadDto> GetBookingByIdAsync(Guid bookingId);
        Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(Guid userId);
        Task<bool> SelectSeatsAsync(Guid bookingId, BookingSeatSelectionDto dto);
        Task<BookingSummaryDto> GetBookingSummaryAsync(Guid bookingId);
    }
}
