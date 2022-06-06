
using Artezanias2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Infrastructure
{
    public class Artezanias2Context: IdentityDbContext<AppUser>
    {

        public Artezanias2Context(DbContextOptions<Artezanias2Context> options) : base(options)
  
    {
       
    } 
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> categories { get; set; }
        public IEnumerable<object> Categories { get; internal set; }
        public DbSet<Product> Products { get; set; }

    }
}

