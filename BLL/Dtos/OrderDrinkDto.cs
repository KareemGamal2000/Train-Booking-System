using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class OrderDrinkDto
    {
        public Guid OrderId { get; set; } 
        public Guid PassengerId { get; set; } 
        public Guid TrainId { get; set; }
        public List<Guid> DrinkIds { get; set; }
        public DateTime OrderTime { get; set; }   
        public decimal TotalPrice { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; } = "Pending"; // حالة الطلب (Pending / Served / Canceled)
    }
}
