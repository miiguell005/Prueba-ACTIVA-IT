using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACTIVA_IT.WEB.Context.Models
{
    public class Album
    {
        [Key]
        public int IdAlbum { set; get; }

        public int Id { set; get; }
        public int IdUsuario { set; get; }
        public DateTime FechaConsulta { set; get; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cancion> Cancion { get; set; }
    }
}
