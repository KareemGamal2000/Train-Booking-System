using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Coach
{
    public interface ICoachRepo
    {
        Task<IEnumerable<Entities.Coach>> GetCoachesAsync();
        Task<Entities.Coach?> GetCoachByIdAsync(int id);
        Task<IEnumerable<Entities.Coach>> GetCoachesByTrainIdAsync(int trainId);
        Task<IEnumerable<Entities.Coach>> GetCoachClass(Entities.Coach coach);

        Task<string> AddCoachAsync(Entities.Coach coach);

        Task<string> UpdateCoachAsync(Entities.Coach coach);

        Task<string> DeleteCoachAsync(int id);
    }
}
