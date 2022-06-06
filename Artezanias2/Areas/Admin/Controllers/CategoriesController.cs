using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artezanias2.Infrastructure;
using Artezanias2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Artezanias2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {


        private readonly Artezanias2Context context;


        public CategoriesController(Artezanias2Context context)
        {
            this.context = context;
        }


        public async Task<IActionResult> Index()
        {


            return View(await context.categories.OrderBy(x => x.Clasificación).ToListAsync());
        }

        //GET Admin/cAategories/Create
        public IActionResult Create() => View();


        //POST Admin/Categories/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Nombre.ToLower().Replace(" ", "-");
                category.Clasificación = 100;
                var slug = await context.categories.FirstOrDefaultAsync(x => x.Slug == category.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "¡La categoría ya existe!!");
                    return View(category);

                }
                context.Add(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "¡Se ha agregado la categoría!";

                return RedirectToAction("Index");
            }
            return View(category);
        }
   
        
        //GET Admin/categorias/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
             Category categories = await context.categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }
        //POST Admin/Categories/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Category category)
        {
            if (ModelState.IsValid)
            {

               
                     category.Slug=category.Nombre.ToLower().Replace(" ", "-");

                var slug = await context.categories.Where(x => x.Id != category.Id).
                    FirstOrDefaultAsync(x => x.Slug == category.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "¡El nombre ya existe!!");
                    return View(category);

                }
                context.Update(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "¡La categoría ha sido editada!";

                return RedirectToAction("index", new { id = category.Id });
            }
            return View(category);
        }

        //GET Admin/categories /delete/1
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await context.categories.FindAsync(id);
            if (category == null)
            {

                TempData["Error"] = "¡La categoria no existe!";

            }
            else
            {
                context.categories.Remove(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "¡La categoria ha sido eliminada!";
            }
            return RedirectToAction("Index");
        }
    
    //POST Admin/categories/Reorder
    [HttpPost]
    public async Task<IActionResult> Reorder(int[] id)
    {
        int count = 1;

        foreach (var categoryId in id)
        {
            Category category = await context.categories.FindAsync(categoryId);
            category.Clasificación = count;
            context.Update(category);
            await context.SaveChangesAsync();
            count++;
        }
        return Ok();
    }
}
}
