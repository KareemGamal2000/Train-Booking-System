using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;



namespace Data.Repository.Passengers
{
    public class PassengerRepo : IPassengerRepo
    {
        private readonly ApplicationDbContext _context;

        public PassengerRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Passenger>> GetAllAsync()
        {
            return await _context.Passengers.ToListAsync();
        }

        public async Task<Passenger?> GetByIdAsync(int id)
        {
            return await _context.Passengers
                                 .FirstOrDefaultAsync(p => p.PassengerId == id);
        }

        public async Task<string> AddAsync(Passenger passenger)
        {
            await _context.Passengers.AddAsync(passenger);
            await _context.SaveChangesAsync();
            return "Passenger Added Successfully";
        }

        public async Task<string> UpdateAsync(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            await _context.SaveChangesAsync();
            return "Passenger Updated Successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var passenger = await _context.Passengers
                                          .FirstOrDefaultAsync(p => p.PassengerId == id);

            if (passenger == null)
                return "Passenger Not Found";

            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();
            return "Passenger Deleted Successfully";
        }
    }
}