using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ACTIVA_IT.WEB.Context;
using ACTIVA_IT.WEB.Context.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ACTIVA_IT.WEB.Models.Albums;
using ACTIVA_IT.WEB.Models.Photos;

namespace ACTIVA_IT.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CancionesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Canciones
        [HttpGet]
        public ActionResult<IEnumerable<Cancion>> GetCancion()
        {
            return null;
        }

        // GET: api/Canciones/5
        [HttpGet("{id}/{idUsuario}")]
        public PhotosDto[] GetCancion(int id, int idUsuario)
        {
            var url = $"https://jsonplaceholder.typicode.com/albums/{id}/photos";
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

                            var photos = JsonConvert.DeserializeObject<List<PhotosDto>>(responseBody);


                            var album = _context.Album.Where(a => a.IdUsuario == idUsuario && a.Id == id).FirstOrDefault();

                            if (album == null) {

                                album = new Album()
                                {
                                    FechaConsulta = DateTime.Now,
                                    IdAlbum = 0,
                                    Id = id,
                                    IdUsuario = idUsuario
                                };

                                _context.Album.Add(album);
                            }
                            else
                                album.FechaConsulta = DateTime.Now;

                            //Guarda el album consultado
                            _context.SaveChanges();


                            var canciones = _context.Cancion.Where(c => c.IdAlbum == album.IdAlbum).ToArray();

                            //No lo lista en las canciones
                            var noListar = canciones.Where(c => c.NoVolverListar).Select(c => c.Id).ToArray();
                            if (noListar.Count() > 0)
                                foreach (var p in photos.Where(_p => noListar.Contains(_p.id)).ToArray())
                                    p.categoria = -2;

                            //No muestra estas canciones en la lista
                            var inapropiado = canciones.Where(c => c.Inapropiado).Select(c => c.Id).ToArray();

                            if (inapropiado.Count() > 0)
                                foreach (var p in photos.Where(_p => inapropiado.Contains(_p.id)).ToArray())
                                    p.categoria = -1;

                            //Marca las canciones como faboritas
                            var faboritos = canciones.Where(c => c.Faborito).Select(c => c.Id).ToArray();

                            if (faboritos.Count() > 0)
                                foreach (var p in photos.Where(_p => faboritos.Contains(_p.id)).ToArray())
                                    p.categoria = 1;

                            //Retorna las photos ordenadas
                            return photos.OrderByDescending(p => p.categoria).ToArray();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw new Exception("Error", ex);
            }
        }

        // POST: api/Canciones
        [HttpPost("{idUsuario}/{faborito}/{inapropiado}/{noListar}")]
        public PhotosDto PostCancion(PhotosDto photos, int idUsuario, bool faborito = false, bool inapropiado = false, bool noListar = false)
        {

            var album = _context.Album.Where(a => a.IdUsuario == idUsuario && a.Id == photos.albumId).FirstOrDefault();

            var cancion = _context.Cancion.Where(c => c.IdAlbum == album.IdAlbum && c.Id == photos.id).FirstOrDefault();

            if (cancion == null)
            {
                cancion = new Cancion()
                {
                    Faborito = faborito,
                    IdAlbum = album.IdAlbum,
                    Id = photos.id,
                    Inapropiado = inapropiado,
                    NoVolverListar = noListar
                };

                _context.Cancion.Add(cancion);
            }
            else
            {                
                cancion.Faborito = faborito;
                cancion.Inapropiado = inapropiado;
                cancion.NoVolverListar = noListar;
            }

            _context.SaveChanges();

            photos.categoria = faborito ? 1 : (inapropiado ? -1 : (noListar ? -2 : 0));

            return photos;
        }

        // DELETE: api/Canciones/5
        [HttpDelete("{id}")]
        public ActionResult<Cancion> DeleteCancion(int id)
        {
            var cancion = _context.Cancion.Find(id);
            if (cancion == null)
            {
                return NotFound();
            }

            _context.Cancion.Remove(cancion);
            _context.SaveChanges();

            return cancion;
        }

        private bool CancionExists(int id)
        {
            return _context.Cancion.Any(e => e.IdCancion == id);
        }
    }
}
