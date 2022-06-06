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
    [Authorize(Roles = "Admin, Editor")]
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly Artezanias2Context context;


        public PagesController(Artezanias2Context context)
        {
            this.context = context;
        }

        //GET Admin/Pages
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.Pages
                                     orderby p.Clasificación
                                     select p;
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }
        //GET Admin/Pages/Details/1
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        //GET Admin/Pages/Create
        public IActionResult Create() => View();



        //POST Admin/Pages/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Titulo.ToLower().Replace(" ", "-");
                page.Clasificación = 100;
                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "¡El titulo ya existe!");
                    return View(page);

                }
                context.Add(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "¡La página ha sido editada!";

                return RedirectToAction("Index");
            }
            return View(page);
        }


        //GET Admin/Pages/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
        //POST Admin/Pages/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "Inicio" :
                     page.Titulo.ToLower().Replace(" ", "-");

                var slug = await context.Pages.Where(x => x.Id != page.Id).
                    FirstOrDefaultAsync(x => x.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "La página ya existe!!!");
                    return View(page);

                }
                context.Update(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "¡La página ha sido editada!";

                return RedirectToAction("Index", new { id = page.Id });
            }
            return View(page);
        }

        //GET Admin/Pages/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            if (page == null)
            {

                TempData["Error"] = "¡La página no existe!";

            }
            else
            {
                context.Pages.Remove(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "¡La página ha sido eliminada!";
            }
            return RedirectToAction("Index");
        }

        //POST Admin/Pages/Reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;

            foreach (var pageId in id)
            {
                Page page = await context.Pages.FindAsync(pageId);
                page.Clasificación = count;
                context.Update(page);
                await context.SaveChangesAsync();
                count++;
            }
            return Ok();
        }
    }
}

