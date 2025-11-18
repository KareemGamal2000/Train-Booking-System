using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDto
{
    public class BookingSeatSelectionDto
    {
        public int CoachId { get; set; }
        public List<int> SelectedSeatIDs { get; set; }
        public decimal PricePerSeat { get; set; }
    }
}
