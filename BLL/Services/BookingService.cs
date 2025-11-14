using Data.Entities;
using Data.Entities.Tickets;
using Data.Repository;
using Data.Repository.Bookings;
using Data.Repository.Seats;
using Data.Repository.Tickets;
using Data.Repository.TripSegmentPrices;
using Data.Repository.TripStops;
using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly ITicketRepo _ticketRepo;
        private readonly ISeatRepo _seatRepo;
        private readonly ITripStopRepo _tripStopRepo;
        private readonly ITripSegmentPriceRepo _segmentPriceRepo;

        public BookingService(
            IBookingRepo bookingRepo,
            ITicketRepo ticketRepo,
            ISeatRepo seatRepo,
            ITripStopRepo tripStopRepo,
            ITripSegmentPriceRepo segmentPriceRepo)
        {
            _bookingRepo = bookingRepo;
            _ticketRepo = ticketRepo;
            _seatRepo = seatRepo;
            _tripStopRepo = tripStopRepo;
            _segmentPriceRepo = segmentPriceRepo;
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
        {
            // 1) إنشاء Booking جديد
            var booking = new Booking
            {
                Booking_ID = Guid.NewGuid(),
                BookingDate = DateTime.UtcNow,
                BookingStatus = "Confirmed",
                UserID = dto.UserID,
                TripID = dto.TripID,
                DepartureStopID = dto.DepartureStopID,
                ArrivalStopID = dto.ArrivalStopID,
                TotalPrice = 0m
            };

            await _bookingRepo.Add(booking);

            decimal totalPrice = 0m;

            // 2) إنشاء التذاكر Tickets
            foreach (var ticketDto in dto.Tickets)
            {
                // السعر حسب Segment + Class
                var price = await GetSegmentPrice(dto.TripID,
                                                  dto.DepartureStopID,
                                                  dto.ArrivalStopID,
                                                  ticketDto.ClassID);

                if (price == null)
                    throw new Exception("No price found for this class & segment.");

                totalPrice += price.Value;

                // حجز الكرسي
                var seat = await _seatRepo.GetSeatByIdAsync(ticketDto.SeatID);
                if (seat == null)
                    throw new Exception("Seat not found.");

                if (seat.IsReserved)
                    throw new Exception("Seat already reserved.");

                await _seatRepo.MarkReservedAsync(ticketDto.SeatID);

                // إنشاء التذكرة
                var ticket = new Ticket
                {
                    Ticket_ID = Guid.NewGuid(),
                    TicketReference = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                    BookingID = booking.Booking_ID,
                    SeatID = ticketDto.SeatID,
                    ClassID = ticketDto.ClassID,
                    Price = price.Value
                };

                await _ticketRepo.Add(ticket);

                booking.Tickets.Add(ticket);
            }

            // 3) تحديث إجمالي السعر
            booking.TotalPrice = totalPrice;
            await _bookingRepo.Update(booking);

            // 4) رجّعي BookingDto
            return new BookingDto
            {
                BookingID = booking.Booking_ID,
                UserID = booking.UserID,
                TripID = booking.TripID,
                BookingDate = booking.BookingDate,
                BookingStatus = booking.BookingStatus,
                DepartureStopID = booking.DepartureStopID,
                ArrivalStopID = booking.ArrivalStopID,
                TotalPrice = totalPrice
            };
        }

        private async Task<decimal?> GetSegmentPrice(int tripId, int startStopId, int endStopId, long classId)
        {
            var prices = await _segmentPriceRepo.GetAll();

            var match = prices.FirstOrDefault(p =>
                p.TripID == tripId &&
                p.StartStopID == startStopId &&
                p.EndStopID == endStopId &&
                p.ClassID == classId);

            return match?.Price;
        }
    }
}