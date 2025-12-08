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
                TotalPrice = booking.TotalPrice,
                UserID = booking.UserID,
                TripID = booking.TripID,
                DepartureStopID = booking.DepartureStopID,
                DepartureStationNameAR = booking.DepartureStop?.Station?.StationNameAR ?? "غير محدد",
                DepartureStationNameEN = booking.DepartureStop?.Station?.StationNameEN ?? "N/A",
                ArrivalStopID = booking.ArrivalStopID,
                ArrivalStationNameAR = booking.ArrivalStop?.Station?.StationNameAR ?? "غير محدد",
                ArrivalStationNameEN = booking.ArrivalStop?.Station?.StationNameEN ?? "N/A",
                Tickets = booking.Tickets?
                    .Select(t => TicketProfile.ToTicketReadDto(t))
                    .ToList() ?? new List<TicketReadDto>()
            };
        }

      
        public static IEnumerable<BookingReadDto> ToBookingReadDtoList(this IEnumerable<Booking> bookings)
        {
            return bookings?.Select(b => b.ToBookingReadDto()) ?? Enumerable.Empty<BookingReadDto>();
        }

        public static BookingSummaryDto ToBookingSummaryDto(this Booking booking)
        {
            if (booking == null)
                return null;

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


    }
}