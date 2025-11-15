using System;
using Data.Context;
using Data.Models; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Repository.MainRepo;

namespace Data.Repository.Class
{
    public class ClassRepo : GenericRepo<Data.Models.Class>, IClassRepo
    {
        public ClassRepo(ApplicationDbContext context) : base(context) { }

        public async Task<Data.Models.Class?> GetClassByNameAsync(string className)
        {
            return await _dbSet.AsNoTracking()
                               .Where(c => c.ClassNameEN.ToLower() == className.ToLower() ||
                                           c.ClassNameAR.ToLower() == className.ToLower())
                               .FirstOrDefaultAsync();
        }

       
        public async Task<IEnumerable<Data.Models.Class>> GetAllClassesWithCoachesAsync()
        {
            return await _dbSet.Include(c => c.Coaches).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Data.Models.Class>> GetAllClassesWithSegmentPricesAsync()
        {
            return await _dbSet.Include(c => c.SegmentPrices).AsNoTracking().ToListAsync();
        }


    }
}
