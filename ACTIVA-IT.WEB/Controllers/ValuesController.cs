using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ACTIVA_IT.WEB.Modulos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ACTIVA_IT.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IConfiguration configuration;
        private UserInfoModulo userInfo;



        // TRAEMOS EL OBJETO DE CONFIGURACIÓN (appsettings.json)
        // MEDIANTE INYECCIÓN DE DEPENDENCIAS.
        public ValuesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var tokenModulo = new TokenModulo(this.configuration);
            var token = tokenModulo.GenerarToken(new Context.Models.Usuario() { IdUsuario = 1, Apellido = "Rod", Nombre = "Mig", Email = "Mig@gmail.com", Password = "Nan" });

            return token;
            //return "value";
        }

        // GET api/values/5
        [HttpGet("Token/{token}")]
        public ActionResult<string> GetToken(string token)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            userInfo = new UserInfoModulo(HttpContext);
            var id = userInfo.GetIdUsuario();

            return token;
            //return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
