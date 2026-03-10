using FintechWallet.Models;
using FintechWallet.Repositories;

namespace FintechWallet.Payments
{
    public interface IPayment
    {
        Task<PaymentResult> CreatePaymentAsync(Payment payment, IRepository<PaymentLog> paymentLogRepository);
        Task<object?> VerifyPaymentAsync(PaymentLog log, PaymentResponse paymentResponse);
    }
}
