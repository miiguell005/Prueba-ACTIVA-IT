using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ACTIVA_IT.WEB.Context;
using ACTIVA_IT.WEB.Context.Models;
using ACTIVA_IT.WEB.Models.Albums;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Security.Claims;
using ACTIVA_IT.WEB.Modulos;
using ACTIVA_IT.WEB.Implementacion.ApiAlbums;

namespace ACTIVA_IT.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private UserInfoModulo userInfo;


        public AlbumesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Albumes/1
        [HttpGet("{pagina}")]
        public async Task<dynamic> GetAlbumAsync(int pagina)
        {
            //Obtiene el idDel usuario por medio del token
            userInfo = new UserInfoModulo(HttpContext);
            var idUsuario = userInfo.GetIdUsuario();

            //Realiza la consulta de una api
            ConsumoApis implementacionModulo = new ConsumoApis();
            var data = await implementacionModulo.GetApi($"https://jsonplaceholder.typicode.com/albums");

            //Deserializa el string a una lista de objetos
            var albumsDto = JsonConvert.DeserializeObject<List<AlbumsDto>>(data);

            albumsDto = albumsDto.ToList();

            var albumes = _context.Album.Where(a => a.IdUsuario == idUsuario).OrderByDescending(a => a.FechaConsulta).ToArray();

            foreach (var a in albumes)
            {
                var album = albumsDto.Where(_a => _a.id == a.Id).FirstOrDefault();
                album.fechaConsulta = a.FechaConsulta;
            }

            //Organiza los albums
            albumsDto = albumsDto.OrderByDescending(o => o.fechaConsulta).ToList();

            //Cuenta la cantidad de albums que hay
            var cantidad = albumsDto.Count();

            return new
            {
                cantidad = cantidad,
                albums = albumsDto.Skip(10 * (pagina - 1)).Take(10)
            };            
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.IdAlbum == id);
        }
    }
}
