using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ACTIVA_IT.WEB.Modulos
{
    public class UserInfoModulo
    {

        public class UserInfo
        {
            public int IdUsuario { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
        }

        HttpContext httpContext;

        public UserInfoModulo(HttpContext _httpContext)
        {
            httpContext = _httpContext;
        }

        /// <summary>
        /// Retorna el Id del usuario en el cual estaba en el token
        /// </summary>
        /// <returns></returns>
        public int GetIdUsuario()
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;

            return int.Parse(identity.Claims.Where(c => c.Type == "IdUsuario").FirstOrDefault().Value);
        }

        /// <summary>
        /// Retorna los datos del usuario que estaba guardado en el token
        /// </summary>
        /// <returns></returns>
        public UserInfo GetUserInfo()
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;

            return new UserInfo()
            {
                IdUsuario = int.Parse(identity.Claims.Where(c => c.Type == "IdUsuario").FirstOrDefault().Value),
                Nombre = identity.Claims.Where(c => c.Type == "Nombre").FirstOrDefault().Value,
                Apellido = identity.Claims.Where(c => c.Type == "Apellido").FirstOrDefault().Value,
                Email = identity.Claims.Where(c => c.Type == "Email").FirstOrDefault().Value,
            };
        }
    }
}
