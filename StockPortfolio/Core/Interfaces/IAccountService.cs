using Microsoft.AspNetCore.Identity;
using StockPortfolio.Api.Models;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(LoginViewModel model);
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<IdentityResult> RegisterAdmin(RegisterViewModel model);
    }
}
