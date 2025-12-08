namespace Domain.Helpers
{
    public class PaymobSettings
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public string CardIntegrationId { get; set; }
        public string WalletIntegrationId { get; set; }
        public string IFrameId { get; set; }
        public string HmacSecret { get; set; }
        public string BaseUrl { get; set; } = "https://accept.paymob.com/api";
        public string CallbackUrl { get; set; } = "https://localhost:7192/FrontEnd/PaymentCallback.html";
    }
}