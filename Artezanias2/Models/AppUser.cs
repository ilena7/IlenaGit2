using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Models
{
    public class AppUser: IdentityUser

    {
        public String Ocupacion { get; set; }


    }
}
