using FintechWallet.Payments.Paystack;
using Microsoft.Extensions.Configuration;

namespace FintechWallet.Payments
{
    public class PaymentFac
    {
        private readonly IConfiguration _config;

        public PaymentFac(IConfiguration config)
        {
            _config = config;
        }

        public IPayment InitiatePayment()
        {
            return new PaystackPayment(_config);
        }
    }
}
