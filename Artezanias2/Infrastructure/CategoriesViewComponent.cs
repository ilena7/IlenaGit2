using Artezanias2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Infrastructure
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly Artezanias2Context context;


        public  CategoriesViewComponent(Artezanias2Context context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetCategoriesAsync();
            return View(categories);
        }

        private Task<List<Category>> GetCategoriesAsync()
        {

            return context.categories.OrderBy(x => x.Clasificación).ToListAsync();


        }
    }
}

