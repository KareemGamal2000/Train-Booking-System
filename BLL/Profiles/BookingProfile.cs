using Data.Models.Tickets;
using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using Domain.Dtos.TicketDtos;
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
                TotalPrice = booking.TotalPrice,
                DepartureStopID = booking.DepartureStopID,  
                ArrivalStopID = booking.ArrivalStopID,     
                Tickets = booking.Tickets?.Select(t => new TicketReadDto
                {
                    Ticket_ID = t.Ticket_ID,
                    Seat_ID = t.SeatID,
                    SeatNumber = t.Seat?.SeatNumber ?? 0,
                    ClassID = t.ClassID,
                    ClassName = t.Class?.ClassNameAR ?? "غير محدد",
                    Price = t.Price
                }).ToList() ?? new List<TicketReadDto>()
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
                NumberOfSeats = booking.Tickets?.Count ?? 0,
                Seats = booking.Tickets?.Select(t => t.SeatID).ToList() ?? new List<int>()
            };
        }

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
                TotalPrice = 0
            };
        }
    }
}