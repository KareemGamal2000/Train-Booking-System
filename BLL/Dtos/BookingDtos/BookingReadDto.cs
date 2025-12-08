using Domain.Dtos.TicketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.BookingDto
{
    public class BookingReadDto
    {
        public Guid BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid UserID { get; set; }
        public int TripID { get; set; }
        public int DepartureStopID { get; set; }
        public int ArrivalStopID { get; set; }  
        public string DepartureStationNameAR { get; set; }
        public string DepartureStationNameEN { get; set; }
        public string ArrivalStationNameAR { get; set; }
        public string ArrivalStationNameEN { get; set; }
 
        public List<TicketReadDto> Tickets { get; set; } = new List<TicketReadDto>();
    }
}
