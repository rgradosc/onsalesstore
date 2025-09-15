using OnSalesStore.ECommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand;
using OnSalesStore.ECommerce.Services.WebAPI.Settings;
using OnSalesStore.ECommerce.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using MediatR;

namespace OnSalesStore.ECommerce.Services.WebAPI.Controllers.v3
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class UsersController : ControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly IMediator _mediator;

        public UsersController(IOptions<AppSetting> appSetting, IMediator mediator)
        {
            _appSetting = appSetting.Value;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] CreateUserTokenCommand command)
        {
            var response = await _mediator.Send(command);

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
            var key = Encoding.ASCII.GetBytes(_appSetting.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDTO.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSetting.Issuer,
                Audience = _appSetting.Audience
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
