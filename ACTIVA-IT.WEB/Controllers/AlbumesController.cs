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

namespace ACTIVA_IT.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlbumesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Albumes/1
        [HttpGet("{id}/{pagina}")]
        public dynamic GetAlbum(int id, int pagina)
        {

            var url = $"https://jsonplaceholder.typicode.com/albums";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return null;

                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            var albumsDto = JsonConvert.DeserializeObject<List<AlbumsDto>>(responseBody);

                            albumsDto = albumsDto.ToList();

                            var albumes = _context.Album.Where(a => a.IdUsuario == id).OrderByDescending(a => a.FechaConsulta).ToArray();

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
                    }
                }
            }
            catch (WebException ex)
            {
                throw new Exception("Error", ex);
            }
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.IdAlbum == id);
        }
    }
}
