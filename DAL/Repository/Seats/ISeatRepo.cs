using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repository.Seats
{
    public interface ISeatRepo
    {
        Task<Seat?> GetSeatByIdAsync(int seatId);
    }
}
