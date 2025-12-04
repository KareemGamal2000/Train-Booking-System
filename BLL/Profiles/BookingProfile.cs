using Data.Models.Tickets;
using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using Domain.Dtos.TicketDtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Profiles
{
    public static class BookingProfile
    {
        // ============= Booking → BookingReadDto =============
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
                TotalPrice = booking.TotalPrice,
                DepartureStopID = booking.DepartureStopID,
                ArrivalStopID = booking.ArrivalStopID,
                Tickets = booking.Tickets?.Select(t => Domain.Profiles.TicketProfile.ToTicketReadDto(t)).ToList() ?? new List<TicketReadDto>()
            };
        }

        // ============= Booking → BookingSummaryDto =============
        public static BookingSummaryDto ToBookingSummaryDto(this Booking booking)
        {
            if (booking == null) return null;

            return new BookingSummaryDto
            {
                BookingId = booking.Booking_ID,
                BookingDate = booking.BookingDate,
                BookingStatus = booking.BookingStatus,
                TotalPrice = booking.TotalPrice,
                TripID = booking.TripID,
                NumberOfSeats = booking.Tickets?.Count ?? 0,
                Seats = booking.Tickets?.Select(t => t.SeatID).ToList() ?? new List<int>()
            };
        }

        // ============= BookingCreateDto → Booking =============
        public static Booking ToBookingModel(this BookingCreateDto dto, Guid userId)
        {
            if (dto == null) return null;

            return new Booking
            {
                Booking_ID = Guid.NewGuid(),
                UserID = userId,
                TripID = dto.TripID,
                DepartureStopID = dto.DepartureStopID,
                ArrivalStopID = dto.ArrivalStopID,
                BookingStatus = "Pending",
                BookingDate = DateTime.UtcNow,
                TotalPrice = 0,
                Tickets = new List<Ticket>()
            };
        }


        // ============= IEnumerable<Booking> → IEnumerable<BookingReadDto> =============
        public static IEnumerable<BookingReadDto> ToBookingReadDtoList(this IEnumerable<Booking> bookings)
        {
            if (bookings == null) return Enumerable.Empty<BookingReadDto>();

            return bookings.Select(b => b.ToBookingReadDto()).ToList();
        }
    }
}