using Artezanias2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Artezanias2.Infrastructure
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly Artezanias2Context context;


        public MainMenuViewComponent(Artezanias2Context context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await GetPagesAsync();
            return View(pages);
        }

        private Task<List<Page>> GetPagesAsync()
        {

            return context.Pages.OrderBy(x => x.Clasificación).ToListAsync();


    }
}
}
