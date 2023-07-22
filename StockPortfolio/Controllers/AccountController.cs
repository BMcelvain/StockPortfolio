using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Api.Models;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Infrastructure.Services;

namespace StockPortfolio.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _accountService.Login(model);

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _accountService.Register(model);

            if (result.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            else if (result.Errors.Any())
            {
                return BadRequest(new Response { Status = "Error", Message = result.Errors.First().Description });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await _accountService.RegisterAdmin(model);
            if (result.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            else if (result.Errors.Any())
            {
                return BadRequest(new Response { Status = "Error", Message = result.Errors.First().Description });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }
    }
}