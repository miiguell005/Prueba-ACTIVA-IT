using ACTIVA_IT.WEB.Context.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ACTIVA_IT.WEB.Modulos
{
    public class TokenModulo
    {

        private readonly IConfiguration configuration;

        public TokenModulo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        /// <summary>
        /// Genera el token con respecto a los datos del usuario logeado
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        /*
        public static string GenerarToken1(Usuario userInfo)
        {
            IdentityModelEventSource.ShowPII = true;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(segurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.IdUsuario.ToString()),
                new Claim("Nombre", userInfo.Nombre),
                new Claim("Apellido", userInfo.Apellido),
                new Claim("Email", userInfo.Email),
                new Claim("IdUsuario", userInfo.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }*/


        public string GenerarToken(Usuario userInfo)
        {
            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"]));

            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("IdUsuario", userInfo.IdUsuario.ToString()),
                new Claim("Nombre", userInfo.Nombre),
                new Claim("Apellido", userInfo.Apellido),
                new Claim("Email", userInfo.Email)
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(24)
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void JwtAtenticacion (this IServiceCollection services)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(segurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            validacionToken = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidateLifetime = true,
                ValidAudience = audience,
                ValidateAudience = true,
                RequireSignedTokens = true,
                IssuerSigningKey = creds.Key
            };

            services.AddAuthentication(op =>
            {
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(op =>
            {
                op.TokenValidationParameters = validacionToken;
                op.IncludeErrorDetails = false;
                op.RequireHttpsMetadata = false;
            });


        }*/
    }
}
