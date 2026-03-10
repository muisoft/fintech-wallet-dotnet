using FintechWallet.Models;
using FintechWallet.Repositories;

namespace FintechWallet.Services
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet> _walletRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public WalletService(IRepository<Wallet> walletRepository, IRepository<Transaction> transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<WalletDto> CreateWalletAsync(Guid userId, CreateWalletDto dto)
        {
            var wallet = new Wallet
            {
                UserId = userId,
                Currency = dto.Currency,
                Balance = 0
            };

            await _walletRepository.AddAsync(wallet);
            return ToDto(wallet);
        }

        public async Task<WalletDto?> GetWalletAsync(Guid id, Guid userId)
        {
            var wallet = await _walletRepository.GetByIdAsync(id);
            if (wallet == null || wallet.UserId != userId) return null;
            return ToDto(wallet);
        }

        public async Task<IEnumerable<WalletDto>> GetUserWalletsAsync(Guid userId)
        {
            var wallets = await _walletRepository.FindItemsByConditionAsync(w => w.UserId == userId);
            return wallets.Select(ToDto);
        }

        public async Task<bool> DepositAsync(Guid walletId, Guid userId, DepositDto dto)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletId);
            if (wallet == null || wallet.UserId != userId) return false;

            wallet.Balance += dto.Amount;

            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                Type = TransactionType.Deposit,
                Amount = dto.Amount,
                Reference = dto.Reference ?? "Deposit"
            };

            await _transactionRepository.AddAsync(transaction);
            await _walletRepository.UpdateAsync(wallet);

            return true;
        }

        public async Task<bool> WithdrawAsync(Guid walletId, Guid userId, WithdrawDto dto)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletId);
            if (wallet == null || wallet.Balance < dto.Amount) return false;

            wallet.Balance -= dto.Amount;

            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                Type = TransactionType.Withdrawal,
                Amount = dto.Amount,
                Reference = dto.Reference ?? "Withdrawal"
            };

            await _transactionRepository.AddAsync(transaction);
            await _walletRepository.UpdateAsync(wallet);

            return true;
        }

        public async Task<bool> TransferAsync(Guid fromWalletId, Guid userId, TransferDto dto)
        {
            var sourceWallet = await _walletRepository.GetByIdAsync(fromWalletId);
            var destWallet = await _walletRepository.GetByIdAsync(dto.ToWalletId);

            if (sourceWallet == null || sourceWallet.UserId != userId || sourceWallet.Balance < dto.Amount) return false;
            if (destWallet == null) return false; // Destination wallet missing

            // Withdraw from Source
            sourceWallet.Balance -= dto.Amount;
            var outTx = new Transaction
            {
                WalletId = sourceWallet.Id,
                Type = TransactionType.TransferOut,
                Amount = dto.Amount,
                Reference = dto.Reference ?? $"Transfer to {destWallet.Id}"
            };

            // Deposit to Destination
            destWallet.Balance += dto.Amount;
            var inTx = new Transaction
            {
                WalletId = destWallet.Id,
                Type = TransactionType.TransferIn,
                Amount = dto.Amount,
                Reference = $"Transfer from {sourceWallet.Id}" // Enforce system ref
            };

            await _transactionRepository.AddAsync(outTx);
            await _walletRepository.UpdateAsync(sourceWallet);

            await _transactionRepository.AddAsync(inTx);
            await _walletRepository.UpdateAsync(destWallet);

            return true;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(Guid walletId, Guid userId)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletId);
            if (wallet == null || wallet.UserId != userId) return Enumerable.Empty<TransactionDto>();

            var transactions = await _transactionRepository.FindItemsByConditionAsync(t => t.WalletId == walletId);
            return transactions.OrderByDescending(t => t.Timestamp).Select(t => new TransactionDto(t.Id, t.Type.ToString(), t.Amount, t.Reference, t.Timestamp));
        }

        private static WalletDto ToDto(Wallet w) => new(w.Id, w.UserId, w.Balance, w.Currency, w.CreatedAt);
    }
}
