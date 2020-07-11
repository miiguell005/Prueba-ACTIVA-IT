using ACTIVA_IT.WEB.Context.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
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
    public static class TokenModulo
    {
        private static string segurityKey = "AC3F091CA34109C5427AFB46CF83B296E2004CA9E55D3B0DA8AA7434F47C69ED";
        private static string issuer = "http://localhost:44365";
        private static string audience = "http://localhost:44365";

        internal static TokenValidationParameters validacionToken;

        /// <summary>
        /// Genera el token con respecto a los datos del usuario logeado
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static string GenerarToken(Usuario userInfo)
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
        }

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


        }
    }
}
