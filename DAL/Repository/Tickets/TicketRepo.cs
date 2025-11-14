using Data.Context;
using Data.Entities.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Tickets
{
    public class TicketRepo : ITicketRepo
    {
        private readonly ApplicationDbContext _context;

        public TicketRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetById(Guid id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task Add(Ticket entity)
        {
            await _context.Tickets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Ticket entity)
        {
            _context.Tickets.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}