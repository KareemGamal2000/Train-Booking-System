using Data.Context;
using Data.Models;
using Data.Models.Tickets;
using Data.Repository.MainRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository.Payment
{
    public class PaymentRepo : GenericRepo<Data.Models.Tickets.Payment>, IPaymentRepo
    {
        public PaymentRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Data.Models.Tickets.Payment?> GetPaymentWithDetailsAsync(Guid paymentId)
        {
            return await GetFirstOrDefaultAsync(filter: p => p.Payment_ID == paymentId, include: new string[] { "Booking.User", "Booking.Trip" });

        }

        public async Task<IEnumerable<Data.Models.Tickets.Payment>> GetPaymentsByBookingIdAsync(Guid bookingId)
        {
            return await GetAllWithOrderingAsync(filter: p => p.BookingID == bookingId, include: null, orderBy: q => q.OrderByDescending(p => p.CreatedAt));
           
        }

        public async Task<Data.Models.Tickets.Payment?> GetPaymentByOrderIdAsync(string orderId)
        {
            return await GetFirstOrDefaultAsync(filter: p => p.PaymobOrderID == orderId, include: new string[] { "Booking" });

        }

        public async Task<bool> UpdatePaymentStatusAsync(Guid paymentId, string status, string? errorMessage = null)
        {
            var payment = await _dbSet.FindAsync(paymentId);
            if (payment == null)
                return false;

            payment.PaymentStatus = status;
            payment.CompletedAt = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(errorMessage))
                payment.ErrorMessage = errorMessage;

            Update(payment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}