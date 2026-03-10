using System.Security.Claims;
using FintechWallet.Models;
using FintechWallet.Repositories;
using FintechWallet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechWallet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/wallets")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly FintechWallet.Payments.PaymentFac _paymentFac;
        private readonly IRepository<PaymentLog> _paymentLogRepository;

        public WalletController(IWalletService walletService, FintechWallet.Payments.PaymentFac paymentFac, IRepository<PaymentLog> paymentLogRepository)
        {
            _walletService = walletService;
            _paymentFac = paymentFac;
            _paymentLogRepository = paymentLogRepository;
        }

        private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDto dto)
        {
            var wallet = await _walletService.CreateWalletAsync(GetUserId(), dto);
            return CreatedAtAction(nameof(GetWallet), new { id = wallet.Id }, wallet);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWallet(Guid id)
        {
            var wallet = await _walletService.GetWalletAsync(id, GetUserId());
            if (wallet == null) return NotFound();
            return Ok(wallet);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyWallets()
        {
            var wallets = await _walletService.GetUserWalletsAsync(GetUserId());
            return Ok(wallets);
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit(Guid id, [FromBody] DepositRequest request)
        {
            var success = await _walletService.DepositAsync(id, GetUserId(), new DepositDto(request.Amount, request.Reference));
            if (!success) return BadRequest("Unable to complete deposit.");
            return Ok(new { Message = "Deposit successful." });
        }

        [HttpPost("fund-with-payment")]
        public async Task<IActionResult> FundWithPayment([FromBody] FundRequest request)
        {
            var pService = _paymentFac.InitiatePayment();

            var payment = new Payment
            {
                UserId = GetUserId(),
                WalletId = request.WalletId,
                Amount = request.Amount,
                PaymentType = "Paystack",
                PaymentRef = request.Reference ?? Guid.NewGuid().ToString()
            };

            var result = await pService.CreatePaymentAsync(payment, _paymentLogRepository);

            if (result != null) return Ok(result);

            return BadRequest("Failed to initialize payment");
        }

        [HttpPost("verify-funding")]
        public async Task<IActionResult> VerifyFunding([FromBody] VerifyPaymentRequest request)
        {
            var log = await _paymentLogRepository.FindByConditionAsync(p => p.PaymentReference == request.PaymentReference);
            if (log == null) return NotFound("Payment record not found");

            var pService = _paymentFac.InitiatePayment();

            var result = await pService.VerifyPaymentAsync(log, new PaymentResponse
            {
                PaymentReference = request.PaymentReference,
                PaymentStatus = request.GatewayStatus
            });

            // if payment succeeds, fund wallet
            if (result != null && result.GetType().GetProperty("Status")?.GetValue(result, null)?.ToString() == "Ok")
            {
                if (log.PaymentStatus != 1)
                {
                    log.PaymentStatus = 1; // Success
                    await _paymentLogRepository.UpdateAsync(log);

                    await _walletService.DepositAsync(log.WalletId, log.UserId, new DepositDto(log.Amount, "Funded via " + log.PaymentType));
                }

                return Ok(new { Message = "Wallet funded successfully via payment gateway", details = result });
            }

            return BadRequest(new { Message = "Payment verification failed or is still pending", details = result });
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
        {
            var success = await _walletService.WithdrawAsync(request.WalletId, GetUserId(), new WithdrawDto(request.Amount, request.Reference));
            if (!success) return BadRequest("Unable to complete withdrawal. Check balance.");
            return Ok(new { Message = "Withdrawal successful." });
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            var success = await _walletService.TransferAsync(request.FromWalletId, GetUserId(), new TransferDto(request.ToWalletId, request.Amount, request.Reference));
            if (!success) return BadRequest("Unable to complete transfer.");
            return Ok(new { Message = "Transfer successful." });
        }

        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactions(Guid id)
        {
            var transactions = await _walletService.GetTransactionsAsync(id, GetUserId());
            return Ok(transactions);
        }
    }

    public record DepositRequest(decimal Amount, string? Reference);
    public record WithdrawRequest(Guid WalletId, decimal Amount, string? Reference);
    public record TransferRequest(Guid FromWalletId, Guid ToWalletId, decimal Amount, string? Reference);
    public record FundRequest(Guid WalletId, decimal Amount, string? Reference);
    public record VerifyPaymentRequest(string PaymentReference, string GatewayStatus);
}
