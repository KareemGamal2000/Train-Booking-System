using Data.Models;
using Domain.Dtos.BookingDtos;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Profiles
{
    public static class SeatProfile
    {
        /// <summary>
        /// تحويل Seat Model إلى SeatDto
        /// </summary>
        public static SeatDto ToSeatDto(this Seat seat, bool isAvailable = true)
        {
            if (seat == null)
                return null;

            return new SeatDto
            {
                SeatID = seat.SeatID,
                SeatNumber = seat.SeatNumber,
                CoachID = seat.CoachID,
                CoachType = seat.Coach?.CoachType ?? string.Empty,
                IsAvailable = isAvailable
            };
        }

        /// <summary>
        /// تحويل قائمة Seats إلى AvailableSeatsDto
        /// </summary>
        public static AvailableSeatsDto ToAvailableSeatsDto(this IEnumerable<Seat> seats, int tripId, long classId)
        {
            if (seats == null)
                seats = Enumerable.Empty<Seat>();

            var seatDtos = seats
                .Select(s => s.ToSeatDto(isAvailable: true))
                .ToList();

            return new AvailableSeatsDto
            {
                TripID = tripId,
                ClassID = classId,
                TotalAvailableSeats = seatDtos.Count,
                Seats = seatDtos
            };
        }
    }
}