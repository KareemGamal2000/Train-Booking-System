using Data.Repository.MainRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Coach
{
    public interface ICoachRepo:IGenericRepo<Data.Models.Coach>
    {
        Task<IEnumerable<Data.Models.Coach>> GetCoachesByClassIdAsync(long classId);

        Task<IEnumerable<Data.Models.Coach>> GetCoachesByClassNameAsync(string classname);

        Task<Data.Models.Coach?> GetCoachWithSeatsAndClassAsync(long coachId);


    }
}
