using Data.Models;
using Data.Models.Tickets;
using Data.Models.Trips;
using Data.Models.Trips;
using Data.Repository.Bookings;
using Data.Repository.Bookings;
using Data.Repository.Class;
using Data.Repository.Coach;
using Data.Repository.Seats;
using Data.Repository.Station;
using Data.Repository.Train;
using Data.Repository.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Task<bool> SaveChangesAsync();
    }
}
