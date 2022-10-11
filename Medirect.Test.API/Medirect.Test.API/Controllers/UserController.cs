using Medirect.Test.API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medirect.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JWT _jWT;

        public UserController(IOptions<JWT> options)
        {
            _jWT = options.Value;
        }

        /// <summary>
        /// Dummy controller to create dummy user. In Production this would have a seperate microservice handling all Identity stuff
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDummyUser()
        {
            var issuer = _jWT.Issuer;
            var audience = _jWT.Audience;
            var key = Encoding.ASCII.GetBytes
            (_jWT.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("Username", "joe.borg")
             }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return Ok(stringToken);
        }
    }
}