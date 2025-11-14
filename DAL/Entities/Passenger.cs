using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Passenger
    {
        public int PassengerId { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int BonusPoints { get; set; }
        public string? ReferralCode { get; set; }
    }
}
