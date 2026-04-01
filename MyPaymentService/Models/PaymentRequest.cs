namespace MyPaymentService.Models
{
    public class PaymentRequest
    {
        public string UserId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
    }
}
