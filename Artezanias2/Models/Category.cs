using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Longitud minima es 2")]
        [RegularExpression(@"^[a -zA-Z-]+$", ErrorMessage = "solo se permiten letras")]
        public string Nombre { get; set; }
        
        public string Slug { get; set; }

        public int Clasificación { get; set; }


    }
}
