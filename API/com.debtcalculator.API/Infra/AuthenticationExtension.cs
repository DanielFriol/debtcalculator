using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using com.debtcalculator.Domain.DTOs;
using com.debtcalculator.Domain.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace com.debtcalculator.API.Infra
{
    public static class AuthenticationExtension
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = (IConfiguration)serviceProvider.GetService(typeof(IConfiguration));

            // var securitySettings = _configuration.GetSection("SecuritySettings");
            // services.Configure<SecuritySettings>(securitySettings);
            // ou:
            var securitySettings = new SecuritySettings();

            new ConfigureFromConfigurationOptions<SecuritySettings>(
                configuration.GetSection("SecuritySettings")
            ).Configure(securitySettings);
            services.AddSingleton(securitySettings);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitySettings.SigningKey));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = securitySettings.RequireHttpsMetadata;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = securitySettings.ValidIssuer,
                    ValidAudience = securitySettings.ValidAudience
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var accessToken = context.SecurityToken as JwtSecurityToken;
                        if (accessToken != null)
                        {
                            ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                            if (identity != null)
                            {
                                identity.AddClaim(new Claim("accessToken", accessToken.RawData));
                            }
                            var userData = context.HttpContext.RequestServices.GetService<DadosSessaoDTO>();
                            userData.DadosDoUsuario = new UsuarioLogadoDTO();
                            userData.DadosDoUsuario.UsuarioId = identity.Claims.FirstOrDefault(c => c.Type.Contains("id"))?.Value;
                            userData.DadosDoUsuario.Nome = identity.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                            userData.DadosDoUsuario.Email = identity.Claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;
                            userData.DadosDoUsuario.IdProfile = long.Parse(identity.Claims.FirstOrDefault(c => c.Type.Contains("idprofile"))?.Value);
                            var nbf = identity.Claims.FirstOrDefault(c => c.Type == "nbf")?.Value;
                            if (!string.IsNullOrEmpty(nbf))
                            {
                                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                                userData.DadosDoUsuario.DataToken = epoch.AddSeconds(Convert.ToInt32(nbf));
                            }
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}