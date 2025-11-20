using Data.Models;
using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Profiles
{
    public static class BookingProfile
    {
        public static BookingReadDto ToBookingReadDto(this Booking booking)
        {
            if (booking == null) return null;

            return new BookingReadDto
            {
                BookingId = booking.Booking_ID,
                BookingDate = booking.BookingDate,
                BookingStatus = booking.BookingStatus,
                TripID = booking.TripID,
                UserID = booking.UserID,
                TotalPrice = booking.TotalPrice
            };
        }

        public static BookingSummaryDto ToBookingSummaryDto(this Booking booking)
        {
            if (booking == null) return null;

            return new BookingSummaryDto
            {
                BookingId = booking.Booking_ID,
                BookingStatus = booking.BookingStatus,
                TotalPrice = booking.TotalPrice,
                NumberOfSeats = booking.Tickets.Count,
                Seats = booking.Tickets.Select(t => t.SeatID).ToList()
            };
        }

        public static Booking ToBookingModel(this BookingCreateDto dto)
        {
            if (dto == null) return null;

            return new Booking
            {
                Booking_ID = Guid.NewGuid(),
                UserID = dto.UserID,
                TripID = dto.TripID,
                DepartureStopID = dto.DepartureStopID,
                ArrivalStopID = dto.ArrivalStopID,
                BookingStatus = "Pending",
                BookingDate = DateTime.UtcNow,
                TotalPrice = 0
            };
        }
    }
}