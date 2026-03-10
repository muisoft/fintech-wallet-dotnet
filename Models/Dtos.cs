namespace FintechWallet.Models
{
    public record RegisterDto(string Username, string Password);
    public record LoginDto(string Username, string Password);
    public record AuthResponse(string Token);

    public record WalletDto(Guid Id, Guid UserId, decimal Balance, string Currency, DateTime CreatedAt);
    public record CreateWalletDto(string Currency = "USD");
    public record DepositDto(decimal Amount, string? Reference);
    public record WithdrawDto(decimal Amount, string? Reference);
    public record TransferDto(Guid ToWalletId, decimal Amount, string? Reference);
    public record TransactionDto(Guid Id, string Type, decimal Amount, string Reference, DateTime Timestamp);
}
