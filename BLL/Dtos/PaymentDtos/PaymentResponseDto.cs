namespace Domain.Dtos.PaymentDtos
{
    public class PaymentResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string PaymentUrl { get; set; }
        public string PaymobOrderID { get; set; }
        public Guid? PaymentID { get; set; }
        public string? PublicKey { get; set; }
        public string? ClientSecret { get; set; }
    }
}