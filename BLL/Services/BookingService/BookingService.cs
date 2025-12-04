using Data.Models;
using Data.Models.Tickets;
using Data.Repository.UnitOfWork;
using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using Domain.Dtos.TicketDtos;
using Domain.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services.BookingService  
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<BookingConfirmationDto> CreateBookingAsync(Guid userId, BookingCreateDto dto)
        {
            // التحقق من الرحلة
            var trip = await _unitOfWork.Trip.GetTripDetailsAsync(dto.TripID);
            if (trip == null)
                throw new Exception("Trip not found");

            var segmentPrice = trip.SegmentPrices?
                .FirstOrDefault(p => p.StartStopID == dto.DepartureStopID
                                  && p.EndStopID == dto.ArrivalStopID
                                  && p.ClassID == dto.ClassID);

            if (segmentPrice == null)
                throw new Exception("لا يوجد سعر محدد لهذا المسار");

            if (segmentPrice.Price == null)
                throw new Exception("سعر المسار غير محدد");

            decimal totalPrice = segmentPrice.Price.Value * dto.NumberOfSeats;

            var departureStop = trip.Stops?.FirstOrDefault(s => s.TripStopID == dto.DepartureStopID);
            var arrivalStop = trip.Stops?.FirstOrDefault(s => s.TripStopID == dto.ArrivalStopID);

            if (departureStop == null || arrivalStop == null)
                throw new Exception("محطات الرحلة غير صحيحة");

            // التحقق من الدرجة
            var classInfo = await _unitOfWork.Class.GetByIdAsync(dto.ClassID);
            if (classInfo == null)
                throw new Exception("الدرجة غير موجودة");

            // إنشاء الحجز باستخدام manual mapping
            var booking = new Booking
            {
                Booking_ID = Guid.NewGuid(),
                UserID = userId,
                TripID = dto.TripID,
                DepartureStopID = dto.DepartureStopID,
                ArrivalStopID = dto.ArrivalStopID,
                BookingDate = DateTime.UtcNow,
                BookingStatus = "Pending",
                TotalPrice = totalPrice,
                Tickets = new List<Ticket>()
            };

            // إضافة التذاكر
            if (dto.SelectedSeatIDs != null && dto.SelectedSeatIDs.Any())
            {
                foreach (var seatId in dto.SelectedSeatIDs)
                {
                    // التحقق من توفر المقعد
                    var isAvailable = await _unitOfWork.Ticket.IsSeatAvailableAsync(seatId, dto.TripID);
                    if (!isAvailable)
                        throw new Exception($"المقعد رقم {seatId} غير متاح");

                    booking.Tickets.Add(new Ticket
                    {
                        Ticket_ID = Guid.NewGuid(),
                        Booking_ID = booking.Booking_ID,
                        SeatID = seatId,
                        ClassID = dto.ClassID,
                        Price = segmentPrice.Price.Value
                    });
                }
            }

            // حفظ الحجز
            await _unitOfWork.Booking.AddBookingAsync(booking);
            await _unitOfWork.SaveChangesAsync();

           
            var savedBooking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(booking.Booking_ID);

            return new BookingConfirmationDto
            {
                BookingID = savedBooking.Booking_ID,
                BookingReference = savedBooking.Booking_ID.ToString().Substring(0, 8).ToUpper(),
                BookingDate = savedBooking.BookingDate,
                BookingStatus = savedBooking.BookingStatus,
                TotalPrice = savedBooking.TotalPrice,
                DepartureStation = departureStop.Station?.StationNameAR ?? "غير محدد",
                ArrivalStation = arrivalStop.Station?.StationNameAR ?? "غير محدد",
                DepartureTime = departureStop.DepartureTime ?? TimeSpan.Zero,
                ArrivalTime = arrivalStop.ArrivalTime ?? TimeSpan.Zero,
                ClassName = classInfo.ClassNameAR ?? "غير محدد",
                Tickets = savedBooking.Tickets?.Select(t => TicketProfile.ToTicketReadDto(t)).ToList() ?? new List<TicketReadDto>()
            };
        }

        // ===================== Cancel Booking =======================
        public async Task<bool> CancelBookingAsync(BookingCancelDto dto)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(dto.BookingId);
            if (booking == null)
                return false;

            booking.BookingStatus = "Cancelled";

            return await _unitOfWork.SaveChangesAsync();
        }

        // ===================== Get Booking By ID =======================
        public async Task<BookingReadDto> GetBookingByIdAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(bookingId);
            
            if (booking == null)
                return null;

            return booking.ToBookingReadDto();
        }

        // ==================== Get User Bookings =========================
        public async Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(Guid userId)
        {
            var bookings = await _unitOfWork.Booking.GetBookingsByUserAsync(userId);

            return bookings.ToBookingReadDtoList();
        }

        // ===================== Select Seats ==============================
        public async Task<bool> SelectSeatsAsync(Guid bookingId, BookingSeatSelectionDto dto)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(bookingId);
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
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(bookingId);
            
            if (booking == null)
                return null;

            return booking.ToBookingSummaryDto();
        }

        // ====================== Confirm Booking ===========================
        public async Task<bool> ConfirmBookingAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(bookingId);
            if (booking == null)
                return false;

            booking.BookingStatus = "Confirmed";
            await _unitOfWork.Booking.UpdateBookingAsync(booking);
            return await _unitOfWork.SaveChangesAsync();
        }
        
        // ====================== Get Available Seats ===========================
        public async Task<AvailableSeatsDto> GetAvailableSeatsAsync(int tripId, long classId, int departureStopId, int arrivalStopId)
        {
            var availableSeats = await _unitOfWork.Seat.GetAvailableSeatsByTripAsync(tripId, classId, departureStopId, arrivalStopId);
            
            return availableSeats.ToAvailableSeatsDto(tripId, classId);
        }
    }
}