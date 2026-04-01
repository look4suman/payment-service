using MyPaymentService.Models;

namespace MyPaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;

        public PaymentService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request)
        {
            // Validate
            if (request.Amount <= 0)
                throw new Exception("Invalid amount");

            // Read config (App Config)
            var maxAmount = decimal.Parse(_config["Payment:MaxAmount"]);

            if (request.Amount > maxAmount)
                throw new Exception("Amount exceeds limit");

            // Read secret (Key Vault)
            var apiKey = _config["PaymentGateway:ApiKey"];

            // Simulate external call
            await Task.Delay(500);

            return new PaymentResponse
            {
                TransactionId = Guid.NewGuid().ToString(),
                Status = "Success"
            };
        }
    }
}
