using Data.Models;
using Data.Models.Tickets;
using Data.Models.Trips;
using Data.Models.Trips;
using Data.Repository.Bookings;
using Data.Repository.Class;
using Data.Repository.Coach;
using Data.Repository.Payment;
using Data.Repository.Seats;
using Data.Repository.Station;
using Data.Repository.Ticket;
using Data.Repository.Train;
using Data.Repository.Trip;
using System;
using System.Threading.Tasks;

namespace Data.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IClassRepo Class { get; }
        ICoachRepo Coach { get; }
        IStationRepo Station { get; }
        ITrainRepo Train { get; }
        ITripRepo Trip { get; }
        IBookingRepo Booking { get; }
        ISeatRepo Seat { get; }
        IPaymentRepo Payment { get; }

        ITicketRepo Ticket {get; }
        Task<bool> SaveChangesAsync();
    }
}
