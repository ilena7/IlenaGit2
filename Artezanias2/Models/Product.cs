using Artezanias2.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Models
{
    public class Product
    {

        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Longitud minima es 2")]
        public string Nombre { get; set; }

        public string Slug { get; set; }

        [Required, MinLength(4, ErrorMessage = "Longitud minima es 4")]
        public string Descripción { get; set; }


        [Column(TypeName="decimal(18,2)")]
        public decimal Precio { get; set; }
       
        [Display(Name = "Category")]
        [Range(1,int.MaxValue,ErrorMessage ="Debes elegir categoría")]
        public int CategoryId { get; set; }

      
        public string Imagen { get; set; }

        [ForeignKey("CategoryId")]
        
        public virtual Category Category { get; set; }
       
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

    }
}
