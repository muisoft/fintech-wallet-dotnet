namespace FintechWallet.Models
{
    public class Payment
    {
        public Guid UserId { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string PaymentRef { get; set; } = string.Empty;
    }

    public class PaymentResult
    {
        public string PaymentType { get; set; } = string.Empty;
        public object? Data { get; set; }
    }

    public class PaymentResponse
    {
        public string PaymentReference { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }
}
