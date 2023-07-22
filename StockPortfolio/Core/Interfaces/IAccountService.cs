using Microsoft.AspNetCore.Identity;
using StockPortfolio.Api.Models;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(LoginModel model);
        Task<IdentityResult> Register(RegisterModel model);
        Task<IdentityResult> RegisterAdmin(RegisterModel model);
    }
}
