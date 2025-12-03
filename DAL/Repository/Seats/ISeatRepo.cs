using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Repository.MainRepo;

namespace Data.Repository.Seats
{
    public interface ISeatRepo:IGenericRepo<Seat>
    {
        Task<Seat?> GetSeatByIdAsync(int seatId);
        Task<IEnumerable<Seat>> GetSeatsByCoachIdAsync(long coachId);
        Task<IEnumerable<Seat>> GetAvailableSeatsByTripAsync(int tripId, long classId, int departureStopId, int arrivalStopId);
        Task<int> GetSeatsCountByCoachIdAsync(long coachId);
        Task<bool> GenerateSeatsForCoachAsync(long coachId, int totalSeats);
    }
}
