using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Class
{
    public interface IClassRepo : IGenericRepo<Data.Models.Class>
    {
        Task<Data.Models.Class?> GetClassByNameAsync(string className);

        Task<IEnumerable<Data.Models.Class>> GetAllClassesWithCoachesAsync();
        Task<IEnumerable<Data.Models.Class>> GetAllClassesWithSegmentPricesAsync();
        
    }
}
