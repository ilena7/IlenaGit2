using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Models
{
    public class UserEdit
    {
       
        [Required,EmailAddress]

        public string Email { get; set; }
        
        [DataType(DataType.Password), MinLength(4, ErrorMessage = "Longitud minima es 4")]

        public string Password { get; set; }

       public  UserEdit() { }
        public UserEdit(AppUser appUser)
        {
            
            Email = appUser.Email;
            Password = appUser.PasswordHash;
        }


    }
}
