using Data.Context;
using Data.Models;
using Data.Models.Tickets;
using Data.Models.Trips;
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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IClassRepo Class { get; private set; }
        public ICoachRepo Coach { get; private set; }
        public IStationRepo Station { get; private set; }
        public ITrainRepo Train { get; private set; }
        public ITripRepo Trip { get; private set; }
        public IBookingRepo Booking { get; private set; }
        public ISeatRepo Seat { get; private set; }






        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Class = new ClassRepo(_context);
            Coach = new CoachRepo(_context);
            Station = new StationRepo(_context);
            Train = new TrainRepo(_context);
            Trip = new TripRepo(_context);
            Booking = new BookingRepo(_context);
            Seat = new SeatRepo(context);


        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
