using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.Data.SqlClient;
using StockPortfolio.Api.Models;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Infrastructure.Data;

namespace StockPortfolio.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AccountRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlServerConnection");
        }

        public async Task<IdentityResult> AddUserToRole(IdentityUser user, string userRole)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await _userManager.AddToRoleAsync(user, userRole);
                return result;
            }
        }

        public async Task<bool> CheckRoleExists(string userRole)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await _roleManager.RoleExistsAsync(userRole);
                return result;
            }
        }
        public async Task<IdentityResult> CreateRole(IdentityRole userRole)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await _roleManager.CreateAsync(userRole);
                return result;
            }
        }

        public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await _userManager.CreateAsync(user, password);
                return result;
            }
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await _userManager.CheckPasswordAsync(user, password);
                return result;
            }
        }

        public async Task<IdentityUser> FindUser(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await _userManager.FindByNameAsync(username);
                return result;
            }
        }

        public async Task<IEnumerable<string>> GetRoles(IdentityUser user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await _userManager.GetRolesAsync(user);
                return result;
            }
        }
    }
}
