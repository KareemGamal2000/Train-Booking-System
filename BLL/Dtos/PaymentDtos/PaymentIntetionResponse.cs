using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.PaymentDtos
{
    public class PaymobIntentionResponse
    {
        public string? client_secret { get; set; }
        public string? public_key { get; set; }
        public string? order_id { get; set; }
        public string? payment_token { get; set; }
        public bool success { get; set; }
        public string? message { get; set; }
    }
}
