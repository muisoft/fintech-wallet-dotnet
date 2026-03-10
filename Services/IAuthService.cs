using FintechWallet.Models;

namespace FintechWallet.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> RegisterAsync(RegisterDto request);
        Task<AuthResponse?> LoginAsync(LoginDto request);
    }
}
