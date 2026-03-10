using FintechWallet.Models;
using FintechWallet.Repositories;
using Microsoft.Extensions.Configuration;

namespace FintechWallet.Payments.Paystack
{
    public class PaystackPayment : IPayment
    {
        private readonly IConfiguration _config;

        public PaystackPayment(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PaymentResult> CreatePaymentAsync(Payment payment, IRepository<PaymentLog> paymentLogRepository)
        {
            // Simulate Paystack Initialization Log
            var paymentLog = new PaymentLog
            {
                UserId = payment.UserId,
                WalletId = payment.WalletId,
                Amount = payment.Amount,
                PaymentReference = payment.PaymentRef,
                PaymentType = "Paystack",
                PaymentStatus = 0 // Pending
            };

            await paymentLogRepository.AddAsync(paymentLog);

            return new PaymentResult
            {
                PaymentType = "Paystack",
                Data = new
                {
                    authorization_url = $"https://checkout.paystack.com/{payment.PaymentRef}",
                    access_code = payment.PaymentRef,
                    reference = payment.PaymentRef
                }
            };
        }

        public async Task<object?> VerifyPaymentAsync(PaymentLog log, PaymentResponse paymentResponse)
        {
            // Simulate Paystack Verification Call
            await Task.Delay(100);

            if (paymentResponse.PaymentStatus.Equals("success", StringComparison.OrdinalIgnoreCase))
            {
                return new
                {
                    Status = "Ok",
                    Amount = log.Amount,
                    Reference = log.PaymentReference
                };
            }

            return new
            {
                Status = "Pending"
            };
        }
    }
}
