﻿using IdentityApi.Entities;
using IdentityApi.Models;
using IdentityApi.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.email))
                return Ok("Email vazio ou nulo");


            if (string.IsNullOrWhiteSpace(login.password))
                return Ok("Senha vazia ou nula");

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                RG = login.rg
            };

            var result = await _userManager.CreateAsync(user, login.password);

            if (result.Errors.Any())
                return BadRequest(result.Errors);

            // program.cs -> RequireConfirmedAccount = true 
            // gerar confirmação de email
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultConfirmEmail = await _userManager.ConfirmEmailAsync(user, code);

            if (resultConfirmEmail.Succeeded)
            {
                var token = new TokenJWTBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("JGHF4W3KHUG2867RUYFSDUIYFDT%DBHAJHKSFFY%"))
                                .AddSubject("identityAPI")
                                .AddIssuer("identityAPI.Security.Bearer")
                                .AddAudience("identityAPI.Security.Bearer")
                                .AddExpiry(5)
                                .Builder();

                var newUser = new
                {
                    id = user.Id,
                    email = user.Email,
                    token = token.value
                };

                return Ok(newUser);
            }

            return Ok("Erro ao confirmar usuário");

        }
    }
}