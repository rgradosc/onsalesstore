using Asp.Versioning;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.UseCases;
using OnSalesStore.ECommerce.Services.WebAPI.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnSalesStore.ECommerce.Services.WebAPI.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class UsersController : Controller
    {
        private readonly IUserApplication _userApplication;
        private readonly AppSetting _options;

        public UsersController(IUserApplication userApplication, IOptions<AppSetting> options)
        {
            _userApplication = userApplication;
            _options = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthDTO userAuthDTO)
        {
            var response = await _userApplication.Authenticate(userAuthDTO);

            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = GenerateToken(response.Data);
                    return Ok(response.Data.Token);
                }
                else
                {
                    return NotFound(response);
                }
            }

            return BadRequest(response);
        }

        private TokenDTO GenerateToken(UserDTO userDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDTO.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _options.Issuer,
                Audience = _options.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return new TokenDTO
            {
                Token = tokenString,
                ExpiredAt = (DateTime)tokenDescriptor.Expires
            };
        }
    }
}
