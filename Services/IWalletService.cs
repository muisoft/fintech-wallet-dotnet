using FintechWallet.Models;

namespace FintechWallet.Services
{
    public interface IWalletService
    {
        Task<WalletDto> CreateWalletAsync(Guid userId, CreateWalletDto dto);
        Task<WalletDto?> GetWalletAsync(Guid id, Guid userId);
        Task<IEnumerable<WalletDto>> GetUserWalletsAsync(Guid userId);
        Task<bool> DepositAsync(Guid walletId, Guid userId, DepositDto dto);
        Task<bool> WithdrawAsync(Guid walletId, Guid userId, WithdrawDto dto);
        Task<bool> TransferAsync(Guid fromWalletId, Guid userId, TransferDto dto);
        Task<IEnumerable<TransactionDto>> GetTransactionsAsync(Guid walletId, Guid userId);
    }
}
