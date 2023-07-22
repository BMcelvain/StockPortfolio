using Microsoft.AspNetCore.Identity;
using System.Net;

namespace StockPortfolio.Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> AddUserToRole(IdentityUser user, string userRole);
        Task<bool> CheckRoleExists(string userRole);
        Task<IdentityResult> CreateRole(IdentityRole userRole);
        Task<IdentityResult> CreateUser(IdentityUser user, string password);
        Task<IdentityUser> FindUser(string username);
        Task<bool> CheckPassword(IdentityUser user, string password);
        Task<IEnumerable<string>> GetRoles(IdentityUser user);
    }
}
