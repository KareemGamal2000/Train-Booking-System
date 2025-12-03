using Data.Context;
using Data.Models;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Coach
{
    public class CoachRepo : GenericRepo<Data.Models.Coach> , ICoachRepo
    {
        public CoachRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Data.Models.Coach>> GetCoachesByClassIdAsync(long classId)
        {

            return await GetAllAsync(filter: c => c.ClassId == classId, include: new string[] { "Class" });
        }

        public async Task<IEnumerable<Data.Models.Coach>> GetCoachesByClassNameAsync(string classname)
        {
            var stationNameLower = classname.ToLower();

            Expression<Func<Data.Models.Coach, bool>> filters =
                c => c.Class != null && (c.Class.ClassNameAR.ToLower().StartsWith(stationNameLower) ||
                   c.Class.ClassNameEN.ToLower().StartsWith(stationNameLower));

            return await GetAllAsync(filter: filters, include: new string[] { "Class" });
               
        }


        public async Task<Data.Models.Coach?> GetCoachWithSeatsAndClassAsync(long coachId)
        {
            return await GetFirstOrDefaultAsync(filter: c => c.Coach_ID == coachId, include: new string[] { "Class","Seats" });
            
        }
    }
}
