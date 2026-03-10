using FintechWallet.Models;
using FintechWallet.Repositories;
using FintechWallet.Services;
using Moq;
using Xunit;

namespace FintechWallet.Tests
{
    public class WalletServiceTests
    {
        private readonly Mock<IRepository<Wallet>> _walletRepositoryMock;
        private readonly Mock<IRepository<Transaction>> _transactionRepositoryMock;
        private readonly WalletService _walletService;

        public WalletServiceTests()
        {
            _walletRepositoryMock = new Mock<IRepository<Wallet>>();
            _transactionRepositoryMock = new Mock<IRepository<Transaction>>();
            _walletService = new WalletService(_walletRepositoryMock.Object, _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task DepositAsync_ShouldReturnTrue_WhenWalletExistsAndBelongsToUser()
        {
            var userId = Guid.NewGuid();
            var walletId = Guid.NewGuid();
            var wallet = new Wallet { Id = walletId, UserId = userId, Balance = 50 };

            _walletRepositoryMock.Setup(r => r.GetByIdAsync(walletId)).ReturnsAsync(wallet);

            var result = await _walletService.DepositAsync(walletId, userId, new DepositDto(50, "Added funds"));

            Assert.True(result);
            Assert.Equal(100, wallet.Balance);
            _transactionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
            _walletRepositoryMock.Verify(r => r.UpdateAsync(wallet), Times.Once);
        }

        [Fact]
        public async Task WithdrawAsync_ShouldReturnFalse_WhenInsufficientFunds()
        {
            var userId = Guid.NewGuid();
            var walletId = Guid.NewGuid();
            var wallet = new Wallet { Id = walletId, UserId = userId, Balance = 20 };

            _walletRepositoryMock.Setup(r => r.GetByIdAsync(walletId)).ReturnsAsync(wallet);

            var result = await _walletService.WithdrawAsync(walletId, userId, new WithdrawDto(50, "Bills"));

            Assert.False(result);
            Assert.Equal(20, wallet.Balance);
            _transactionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Never);
        }
    }
}
