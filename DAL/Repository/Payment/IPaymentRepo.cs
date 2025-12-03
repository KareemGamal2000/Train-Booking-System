using Data.Models.Tickets;
using Data.Repository.MainRepo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Payment
{
    public interface IPaymentRepo : IGenericRepo<Data.Models.Tickets.Payment>
    {
        Task<Data.Models.Tickets.Payment?> GetPaymentWithDetailsAsync(Guid paymentId);
        Task<IEnumerable<Data.Models.Tickets.Payment>> GetPaymentsByBookingIdAsync(Guid bookingId);
        Task<Data.Models.Tickets.Payment?> GetPaymentByOrderIdAsync(string orderId);
        Task<bool> UpdatePaymentStatusAsync(Guid paymentId, string status, string? errorMessage = null);
    }
}