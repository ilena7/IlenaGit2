using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Models
{
    public class User
    {
        [Required, MinLength(2, ErrorMessage = "Longitud minima es 2")]

        public string UserName { get; set; }
        [Required,EmailAddress]

        public string Email { get; set; }
        [DataType(DataType.Password),Required, MinLength(4, ErrorMessage = "Longitud minima es 4")]

        public string Password { get; set; }

       
      


    }
}
