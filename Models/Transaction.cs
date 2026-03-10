namespace FintechWallet.Models
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        TransferIn,
        TransferOut
    }

    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WalletId { get; set; }
        public Wallet? Wallet { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
