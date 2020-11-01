using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Infra;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace com.debtcalculator.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class SignInController : ControllerBase
    {

        
        private readonly IMediator _mediator;
        private readonly SecuritySettings _securitySettings;
        private readonly IWebHostEnvironment _environment;

        public SignInController(
            IMediator mediator,
            SecuritySettings securitySettings,
            IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _securitySettings = securitySettings;
            _environment = environment;
        }
        
        /// <summary>
        /// Autentica um user.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /Users
        ///     {
        ///       "email": "email@domain.com.br",
        ///       "password": "123456",
        ///     }
        ///
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Access Token</returns>
        /// <response code="200">Uma vez autenticado, retorna o Access Token</response>
        /// <response code="400">email e senha sejam nulo</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Domain.Mediator.SignIn.Request request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return generateToken(response.Result as Domain.Models.UserModel);
        }


        private IActionResult generateToken(Domain.Models.UserModel user)
        {

            var claims = new List<Claim>() {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("email", user.Email),
                new Claim("idprofile", user.IdProfile.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.SigningKey));
            var siginCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: _securitySettings.ValidIssuer,
                    audience: _securitySettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(_securitySettings.Expires),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: siginCredentials
                );

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                environment = _environment.EnvironmentName
            });
        }
    }
}