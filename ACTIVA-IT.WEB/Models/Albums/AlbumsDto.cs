using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACTIVA_IT.WEB.Models.Albums
{
    public class AlbumsDto
    {
        [Key]
        public int userId { get; set; }

        public int id { get; set; }

        public string title { get; set; }

        public Nullable<DateTime> fechaConsulta { get; set; }
    }
}
