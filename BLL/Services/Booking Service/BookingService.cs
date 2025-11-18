using AutoMapper;
using Data.Models;
using Data.Models.Tickets;
using Data.Repository.UnitOfWork;
using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Booking_Service
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ================ Create Booking =====================
        public async Task<Guid> CreateBookingAsync(BookingCreateDto dto)
        {
            var trip = await _unitOfWork.Trip.GetByIdAsync(dto.TripID);
            if (trip == null)
                throw new Exception("Trip not found");

            var newBooking = new Booking
            {
                Booking_ID = Guid.NewGuid(),
                BookingDate = DateTime.UtcNow,
                BookingStatus = "Pending",
                UserID = dto.UserID,
                TripID = dto.TripID,
                DepartureStopID = dto.DepartureStopID,
                ArrivalStopID = dto.ArrivalStopID,
                TotalPrice = 0
            };

            await _unitOfWork.Booking.AddBookingAsync(newBooking);
            await _unitOfWork.SaveChangesAsync();

            return newBooking.Booking_ID;
        }

        // ================= Cancel Booking =====================
        public async Task<bool> CancelBookingAsync(BookingCancelDto dto)
        {
            var booking = await _unitOfWork.Booking.GetBookingByIdAsync(dto.BookingId);
            if (booking == null)
                return false;

            booking.BookingStatus = "Cancelled";
            return await _unitOfWork.SaveChangesAsync();
        }

        // =================== Get Booking =======================
        public async Task<BookingReadDto> GetBookingByIdAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingByIdAsync(bookingId);

            if (booking == null)
                return null;

            return _mapper.Map<BookingReadDto>(booking);
        }

        // ============= Get All Bookings for a User =============
        public async Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(Guid userId)
        {
            var bookings = await _unitOfWork.Booking.GetBookingsByUserAsync(userId);
            return bookings.Select(b => _mapper.Map<BookingReadDto>(b)).ToList();
        }

        // =================== Select Seats =======================
        // ⭐ بعد التعديل — مفيش BookingId جوا الـ DTO
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
                    Price = dto.PricePerSeat
                });

                total += dto.PricePerSeat;
            }

            booking.TotalPrice = total;
            booking.BookingStatus = "Confirmed";

            return await _unitOfWork.SaveChangesAsync();
        }

        // =================== Summary ============================
        public async Task<BookingSummaryDto> GetBookingSummaryAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingByIdAsync(bookingId);

            if (booking == null)
                return null;

            var dto = _mapper.Map<BookingSummaryDto>(booking);

            dto.Seats = booking.Tickets.Select(t => t.SeatID).ToList();  // ⭐ int
            dto.NumberOfSeats = dto.Seats.Count;

            return dto;
        }
    }
}