using System.ComponentModel.DataAnnotations;

namespace FintechWallet.Models
{
    public class PaymentLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentReference { get; set; } = string.Empty;
        public string PaymentType { get; set; } = string.Empty; // e.g., paystack, monnify
        public int PaymentStatus { get; set; } = 0; // 0=Pending, 1=Success, 2=Failed
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
