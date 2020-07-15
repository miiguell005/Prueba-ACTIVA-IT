using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ACTIVA_IT.WEB.Context;
using ACTIVA_IT.WEB.Context.Models;
using Microsoft.Extensions.Configuration;
using ACTIVA_IT.WEB.Modulos;

namespace ACTIVA_IT.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration configuration;

        public UsuariosController(AppDbContext context, IConfiguration _configuration)
        {
            _context = context;
            configuration = _configuration;
        }

        // GET: api/Usuarios
        [HttpGet]
        public dynamic[] GetUsuario()
        {
            var usuarios = _context.Usuario.Select(u => new { u.IdUsuario, u.Nombre, u.Apellido }).ToArray();

            return usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet("Token/{id}")]
        public dynamic GetToken(int id)
        {
            var usuario = _context.Usuario.Find(id);

            if (usuario == null)
                throw new Exception("No se encontro ningun usuario con Id " + id);

            var tokenModulo = new TokenModulo(this.configuration);
            var token = tokenModulo.GenerarToken(usuario);

            return new { Token = token };
        }

    }
}
