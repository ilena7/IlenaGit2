
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Infrastructure
{
    public class Artezanias2Context: DbContext
    {

        public Artezanias2Context(DbContextOptions<Artezanias2Context> options) : base(options)
  
    {

    }
    
    
}
