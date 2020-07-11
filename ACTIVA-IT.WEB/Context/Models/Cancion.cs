using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACTIVA_IT.WEB.Context.Models
{
    public class Cancion
    {
        [Key]
        public int IdCancion { get; set; }

        public int Id { get; set; }

        public int IdAlbum { get; set; }

        public bool Faborito { get; set; }
        
        public bool Inapropiado { get; set; }

        public bool NoVolverListar { get; set; }

        public virtual Album Album { get; set; }
    }
}
