using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Artezanias2.Infrastructure;
using Artezanias2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




namespace Artezanias2.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private readonly Artezanias2Context context;


        public ProductsController(Artezanias2Context context)
        {
            this.context = context;
        }

        //get/products
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var products = context.Products.OrderByDescending(x => x.Id)
                
                .Skip((p - 1) * pageSize)
               .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Count() / pageSize);
            return View(await products.ToListAsync());

        }
        //get/products/category
        public async Task<IActionResult> ProductsByCategory(string categorySlug, int p = 1)
        {
            Category category = await context.categories.Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();
            if (category == null) return RedirectToAction("Index");


            int pageSize = 2;
            var products = context.Products.OrderByDescending(x => x.Id)
                .Where(x => x.CategoryId== category.Id)
                .Skip((p - 1) * pageSize)
               .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Where(x => x.CategoryId==
            category.Id).Count() / pageSize);
            ViewBag.CategoryNombre = category.Nombre;
            ViewBag.CategorySlug = categorySlug;

            return View(await products.ToListAsync());

        }
    }
}