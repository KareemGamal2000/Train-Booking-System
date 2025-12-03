using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.SeedData
{
    public static class SeatGenerator
    {
        public static async Task GenerateSeatsForCoachesAsync(ApplicationDbContext context)
        {
            try
            {
                var coaches = await context.Coaches
                    .Include(c => c.Seats)
                    .Where(c => !c.IsSeatless && c.Seats.Count == 0)
                    .ToListAsync();

                if (!coaches.Any())
                {
                    Console.WriteLine(" No coaches found that need seat generation");
                    return;
                }

                foreach (var coach in coaches)
                {
                    Console.WriteLine($"🔧 Generating {coach.TotalSeats} seats for Coach ID: {coach.Coach_ID}");

                    for (int i = 1; i <= coach.TotalSeats; i++)
                    {
                        var seat = new Seat
                        {
                            SeatNumber = i,
                            CoachID = coach.Coach_ID
                        };

                        context.Seats.Add(seat);
                    }
                }

                // حفظ جميع المقاعد دفعة واحدة
                int savedCount = await context.SaveChangesAsync();
                Console.WriteLine($" Generated {savedCount} seats for {coaches.Count} coaches");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error generating seats: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"   Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
}