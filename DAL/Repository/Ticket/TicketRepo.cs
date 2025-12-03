using Data.Context;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository.Ticket
{
    public class TicketRepo : GenericRepo<Data.Models.Tickets.Ticket>, ITicketRepo
    {
        public TicketRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsSeatAvailableAsync(int seatId, int tripId)
        {
            Expression<Func<Data.Models.Tickets.Ticket, bool>> filter = t =>
                  t.SeatID == seatId &&
                  t.Booking.TripID == tripId &&
                  t.Booking.BookingStatus == "Confirmed";

            bool isBooked = await AnyAsync(filter: filter);

            // إرجاع عكس النتيجة: إذا كان محجوزاً (true)، فهو غير متاح (false)
            return !isBooked;
        }
        public async Task<IEnumerable<int>> GetBookedSeatIdsAsync(int tripId, long classId, int departureStopId, int arrivalStopId)
        {
            Expression<Func<Data.Models.Tickets.Ticket, bool>> filters = t =>
                t.Booking.TripID == tripId
                && t.Booking.BookingStatus != "Cancelled"
                && t.ClassID == classId
                && t.SeatID != 0
                && ((t.Booking.DepartureStopID <= departureStopId && t.Booking.ArrivalStopID > departureStopId)
                    || (t.Booking.DepartureStopID < arrivalStopId && t.Booking.ArrivalStopID >= arrivalStopId)
                    || (t.Booking.DepartureStopID >= departureStopId && t.Booking.ArrivalStopID <= arrivalStopId));

            string[] includes = new string[] { "Booking" };

            Expression<Func<Data.Models.Tickets.Ticket, int>> selector = t => t.SeatID;

            var bookedSeatIds = await GetAllWithSelectAsync<int>(
                filter: filters,
                selector: selector,
                include: includes
            );

            return bookedSeatIds.Distinct();
        }
    }
}