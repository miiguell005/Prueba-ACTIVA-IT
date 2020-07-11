using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACTIVA_IT.WEB.Models.Photos
{
    public class PhotosDto
    {
        [Key]
        public int albumId { get; set; }

        public int id { get; set; }

        public string title { get; set; }

        public string url { get; set; }

        public string thumbnailUrl { get; set; }

        public int categoria { get; set; }

    }
}
