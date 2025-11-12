using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class BonusDto
    {
        public Guid PassengerId { get; set; }
        public int Points { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
