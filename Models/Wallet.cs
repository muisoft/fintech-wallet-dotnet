namespace FintechWallet.Models
{
    public class Wallet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public decimal Balance { get; set; } = 0;
        public string Currency { get; set; } = "USD";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
