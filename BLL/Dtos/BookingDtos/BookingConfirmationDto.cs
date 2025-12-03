using System;
using System.Collections.Generic;

namespace Domain.Dtos.BookingDto
{
    public class BookingConfirmationDto
    {
        public Guid BookingID { get; set; }
        public string BookingReference { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; }
        public decimal TotalPrice { get; set; }
        
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        
        public string ClassName { get; set; }
        public List<TicketDtos.TicketReadDto> Tickets { get; set; } = new List<TicketDtos.TicketReadDto>();
    }

}