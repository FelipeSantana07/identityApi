using IdentityApi.Entities;
using IdentityApi.Models;
using IdentityApi.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TokenController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        public async Task<IActionResult> CreateToken([FromBody] InputLoginRequest Input)
        {
            if (string.IsNullOrWhiteSpace(Input.Email) || string.IsNullOrWhiteSpace(Input.Password))
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);

            if (result.Succeeded) 
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("identityAPI")
                    .AddIssuer("identityAPI.Security.Bearer")
                    .AddAudience("identityAPI.Security.Bearer")
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);

            }

            return Unauthorized();
        } 
    }
}
