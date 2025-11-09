using System;
using Data.Context;
using Data.Entities; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Class
{
    public class ClassRepo
    {
        private readonly ApplicationDbContext _context;

        public ClassRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Entities.Class>> GetAllClassesAsync()
        {
            return await _context.Classes.ToListAsync();
        }


    }
}
