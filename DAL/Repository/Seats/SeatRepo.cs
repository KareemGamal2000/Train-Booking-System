using Data.Context;
using Data.Models;
using Data.Repository.MainRepo;
using Data.Repository.Ticket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Seats
{
    public class SeatRepo : GenericRepo<Seat> ,ISeatRepo
    {
        private readonly ITicketRepo _ticketRepo;

        public SeatRepo(ApplicationDbContext context, ITicketRepo ticketRepo) : base(context)
        {
            _ticketRepo = ticketRepo;
        }

        public SeatRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Seat?> GetSeatByIdAsync(int seatId)
        {
            return await GetFirstOrDefaultAsync(filter: s => s.SeatID == seatId, include: new string[] { "Coach.Class" });
        }

        public async Task<IEnumerable<Seat>> GetSeatsByCoachIdAsync(long coachId)
        {
            return await GetAllWithOrderingAsync(
                filter: s => s.CoachID == coachId,
                include: new string[] { "Coach.Class" },
                orderBy: q => q.OrderBy(s => s.SeatNumber)
            ); 
        }

        public async Task<int> GetSeatsCountByCoachIdAsync(long coachId)
        {
            return await CountAsync(filter: s => s.CoachID == coachId);
        }
        public async Task<bool> GenerateSeatsForCoachAsync(long coachId, int totalSeats)
        {
            try
            {
                var coach = await _context.Coaches
                    .Include(c => c.Seats)
                    .FirstOrDefaultAsync(c => c.Coach_ID == coachId);
                  

                if (coach == null || coach.IsSeatless || coach.Seats.Any())
                    return false;

                // توليد المقاعد
                for (int i = 1; i <= totalSeats; i++)
                {
                    await _context.Seats.AddAsync(new Seat
                    {
                        SeatNumber = i,
                        CoachID = coachId
                    });
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<Seat>> GetAvailableSeatsByTripAsync(int tripId, long classId, int departureStopId, int arrivalStopId)
        {
             var trip = await _context.Trips 
                .AsNoTracking()
                .Include(t => t.Train)
                    .ThenInclude(train => train.TrainCoaches)
                        .ThenInclude(tc => tc.Coach)
                            .ThenInclude(c => c.Seats)
                .Include(t => t.Train)
                    .ThenInclude(train => train.TrainCoaches)
                        .ThenInclude(tc => tc.Coach)
                            .ThenInclude(c => c.Class)
                .FirstOrDefaultAsync(t => t.TripID == tripId);
               

            if (trip == null)
                return Enumerable.Empty<Seat>();

            // الحصول على العربات النشطة للدرجة المحددة
            var coaches = trip.Train.TrainCoaches
                .Where(tc => tc.Coach.ClassId == classId && tc.IsActive)
                .Select(tc => tc.Coach)
                .ToList();

            // الحصول على المقاعد المحجوزة
           var bookedSeatIds = await _ticketRepo.GetBookedSeatIdsAsync(tripId, classId, departureStopId, arrivalStopId);
            // جمع جميع المقاعد المتاحة
            var availableSeats = new List<Seat>();

            foreach (var coach in coaches)
            {
                // توليد المقاعد تلقائياً إذا لم تكن موجودة
                if (!coach.IsSeatless && !coach.Seats.Any())
                {
                    await GenerateSeatsForCoachAsync(coach.Coach_ID, coach.TotalSeats);
                    await _context.Entry(coach).Collection(c => c.Seats).LoadAsync();
                }

                // إضافة المقاعد المتاحة فقط
                var coachAvailableSeats = coach.Seats
                    .Where(s => !bookedSeatIds.Contains(s.SeatID))
                    .ToList();

                availableSeats.AddRange(coachAvailableSeats);
            }

            return availableSeats;
        }
    }
}