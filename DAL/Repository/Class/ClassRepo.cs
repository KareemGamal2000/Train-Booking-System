using System;
using Data.Context;
using Data.Models; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore.Query;

namespace Data.Repository.Class
{
    public class ClassRepo : GenericRepo<Data.Models.Class>, IClassRepo
    {
        public ClassRepo(ApplicationDbContext context) : base(context) { }

        public async Task<Data.Models.Class?> GetClassByNameAsync(string className)
        {
            return await GetFirstOrDefaultAsync(filter: c => c.ClassNameEN.ToLower() == className.ToLower() || c.ClassNameAR.ToLower() == className.ToLower() , include: null);
        }

       
        public async Task<IEnumerable<Data.Models.Class>> GetAllClassesWithCoachesAsync()
        {
            string[] includes = new string[]  {"Class.Coaches"};
            return await GetAllAsync(filter: null ,include: includes);
        }

        public async Task<IEnumerable<Data.Models.Class>> GetAllClassesWithSegmentPricesAsync()
        {
            string[] includes = new string[] { "Class.SegmentPrices" };
            return await GetAllAsync(filter: null, include: includes);
        }


    }
}
