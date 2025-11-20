using Data.Models;
using Data.Models.Tickets;
using Data.Repository.UnitOfWork;
using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using Domain.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Profiles;


namespace Domain.Services.Booking_Service
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ===================== Create Booking =======================
        public async Task<Guid> CreateBookingAsync(BookingCreateDto dto)
        {
            var trip = await _unitOfWork.Trip.GetByIdAsync(dto.TripID);
            if (trip == null)
                throw new Exception("Trip not found");

            var newBooking = dto.ToBookingModel();

            await _unitOfWork.Booking.AddBookingAsync(newBooking);
            await _unitOfWork.SaveChangesAsync();

            return newBooking.Booking_ID;
        }

        // ===================== Cancel Booking =======================
        public async Task<bool> CancelBookingAsync(BookingCancelDto dto)
        {
            var booking = await _unitOfWork.Booking.GetBookingByIdAsync(dto.BookingId);
            if (booking == null)
                return false;

            booking.BookingStatus = "Cancelled";

            return await _unitOfWork.SaveChangesAsync();
        }

        // ===================== Get Booking By ID =======================
        public async Task<BookingReadDto> GetBookingByIdAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingByIdAsync(bookingId);
            return booking.ToBookingReadDto();
        }

        // ==================== Get User Bookings =========================
        public async Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(Guid userId)
        {
            var bookings = await _unitOfWork.Booking.GetBookingsByUserAsync(userId);

            return bookings
                .Select(b => b.ToBookingReadDto())
                .ToList();
        }

        // ===================== Select Seats ==============================
        public async Task<bool> SelectSeatsAsync(Guid bookingId, BookingSeatSelectionDto dto)
        {
            var booking = await _unitOfWork.Booking.GetBookingByIdAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            booking.Tickets.Clear();

            decimal total = 0;

            foreach (var seatId in dto.SelectedSeatIDs)
            {
                var seat = await _unitOfWork.Seat.GetSeatByIdAsync(seatId);
                if (seat == null)
                    throw new Exception($"Seat {seatId} not found");

                booking.Tickets.Add(new Ticket
                {
                    Ticket_ID = Guid.NewGuid(),
                    Booking_ID = booking.Booking_ID,
                    SeatID = seatId,
                    Price = dto.PricePerSeat,
                });

                total += dto.PricePerSeat;
            }

            booking.TotalPrice = total;
            booking.BookingStatus = "Confirmed";

            return await _unitOfWork.SaveChangesAsync();
        }

        // ====================== Booking Summary ===========================
        public async Task<BookingSummaryDto> GetBookingSummaryAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingByIdAsync(bookingId);
            return booking.ToBookingSummaryDto();
        }
    }
}