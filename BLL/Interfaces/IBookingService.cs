using Data.Entities;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(CreateBookingDto dto);
    }
}
