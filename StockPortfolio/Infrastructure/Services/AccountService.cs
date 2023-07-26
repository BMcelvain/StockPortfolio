using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StockPortfolio.Api.Models;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;
using StockPortfolio.Infrastructure.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockPortfolio.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;


        public AccountService(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }
        public async Task<LoginResponse> Login(LoginViewModel model)
        {
            var user = await _accountRepository.FindUser(model.Username);
            if (user != null && await _accountRepository.CheckPassword(user, model.Password))
            {
                var userRoles = await _accountRepository.GetRoles(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GenerateToken(authClaims);

                return new LoginResponse()
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
            }

            return null;
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            var userExists = await _accountRepository.FindUser(model.Username);
            if (userExists != null)
            {
                var error = new IdentityError[] { new IdentityError { Code = "UserExists", Description = "User already exists!" } };   
                var errorResult = IdentityResult.Failed(error);

                return errorResult;
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _accountRepository.CreateUser(user, model.Password);

            return result;
        }

        public async Task<IdentityResult> RegisterAdmin(RegisterViewModel model)
        {
            var userExists = await _accountRepository.FindUser(model.Username);
            if (userExists != null)
            {
                var error = new IdentityError[] { new IdentityError { Code = "UserExists", Description = "User already exists!" } };
                var errorResult = IdentityResult.Failed(error);

                return errorResult;
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _accountRepository.CreateUser(user, model.Password);

            // Create Admin Role, if it doens't exist. Else, add user to Admin Role.
            if (!await _accountRepository.CheckRoleExists(UserRoles.Admin))
            {
                result = await _accountRepository.CreateRole(new IdentityRole(UserRoles.Admin));
            }
            else
            {
                result = await _accountRepository.AddUserToRole(user, UserRoles.Admin);
            }

            // Create User Role, if it doens't exist. Else, add user to User Role. 
            if (!await _accountRepository.CheckRoleExists(UserRoles.User))
            {
                result = await _accountRepository.CreateRole(new IdentityRole(UserRoles.User));
            }
            else
            {
                result = await _accountRepository.AddUserToRole(user, UserRoles.User);
            }

            return result;
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:ValidIssuer"],
                audience: _configuration["JwtSettings:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
