using FinalEvidence.Models;
using FinalEvidence.ViewModels.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace R52_M12_Class_05_Work_01.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;
        public readonly CarInformationDbContext db;
        public AccountController(UserManager<IdentityUser> userManager, IConfiguration config, CarInformationDbContext db)
        {
            this.userManager = userManager;
            this.config = config;
            this.db = db;
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            //Thread.Sleep(2000);
            var user = await userManager.FindByNameAsync(model.Username.ToUpper());

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                //var roles = await userManager.GetRolesAsync(user);
                var signingKey =
                  Encoding.UTF8.GetBytes(config["Jwt:SigningKey"] ?? "");
                var expiryDuration = int.Parse(config["Jwt:ExpiryInMinutes"] ?? "");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = null,              // Not required as no third-party is involved
                    Audience = null,            // Not required as no third-party is involved
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                    Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("username",user.UserName ?? ""),
                        new Claim("expires", DateTime.UtcNow.AddMinutes(expiryDuration).ToString("yyyy-MM-ddTHH:mm:ss"))
                    }
                    ),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = jwtTokenHandler.WriteToken(jwtToken);
                return Ok(
                  new
                  {
                      token,
                      expiration = jwtToken.ValidTo

                  });
            }
            return BadRequest();
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Username = user.UserName });
            }
            return BadRequest("Regiteration failed");

        }
    }
}
