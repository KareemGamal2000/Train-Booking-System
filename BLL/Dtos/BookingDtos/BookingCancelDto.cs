using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDto
{
    public class BookingCancelDto
    {
        public Guid BookingId { get; set; }
        public string Reason { get; set; }
    }
}
