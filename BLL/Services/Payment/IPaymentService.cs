using Domain.Dtos.PaymentDtos;

namespace Domain.Services.Payment
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> InitiatePaymentAsync(PaymentRequestDto request, Guid userId);
        Task<bool> ProcessCallbackAsync(PaymobCallbackDto callback);
        Task<bool> RefundPaymentAsync(Guid paymentId);

        Task<Data.Models.Tickets.Payment> GetPaymentByBookingAsync(Guid bookingId);


    }
}