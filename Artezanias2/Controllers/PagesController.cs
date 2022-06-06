using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artezanias2.Infrastructure;
using Artezanias2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Artezanias2.Controllers
{
    public class PagesController : Controller
    {
        private readonly Artezanias2Context context;


        public PagesController(Artezanias2Context context)
        {
            this.context = context;
        }
        //get/or/slug
        public async Task<IActionResult> Page(string slug)
        {
            if (slug == null)
            {
                return View(await context.Pages.Where(x => x.Slug == "inicio").FirstOrDefaultAsync());
        
            }

            Page page = await context.Pages.Where(x => x.Slug == slug).FirstOrDefaultAsync();
            if (page==null)
            {
                return NotFound();
                    
            }
            

        return View(page);
        }
    }
}