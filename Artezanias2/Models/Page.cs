using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Models
{
    public class Page
    {

        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Longitud minima es 2")]
        public string Titulo { get; set; }
    
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Longitud minima es 4")]
        public string Contenido { get; set; }

        public int Clasificación { get; set; }

    }
}

