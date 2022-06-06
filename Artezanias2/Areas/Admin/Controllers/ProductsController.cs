using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Artezanias2.Infrastructure;
using Artezanias2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Artezanias2.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin, Editor")]
    [Area("Admin")]
    public class ProductsController : Controller
    {


        private readonly Artezanias2Context context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(Artezanias2Context context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;


        }


        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var products = context.Products.OrderByDescending(x => x.Id)
                .Include(x => x.Category)
                .Skip((p - 1) * pageSize)
               .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Count() / pageSize);
            return View(await products.ToArrayAsync());

        }
        //GET Admin/category/Details/1
        public async Task<IActionResult> Details(int id)
        {
            Product product = await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //GET Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.categories.OrderBy(x => x.Clasificación), "Id", "Nombre");
            return View();
        }
        //POST Admin/Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.CategoryId = new SelectList(context.categories.OrderBy(x => x.Clasificación), "Id", "Nombre");

            if (ModelState.IsValid)
            {
                product.Slug = product.Nombre.ToLower().Replace(" ", "-");

                var slug = await context.Products.FirstOrDefaultAsync(x => x.Slug == product.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "¡El producto ya existe!");
                    return View(product);

                }
                string imagenNombre = "noimagen.png";


                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/product");
                    imagenNombre = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imagenNombre);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();


                }
                product.Imagen = imagenNombre;

                context.Add(product);
                await context.SaveChangesAsync();

                TempData["Success"] = "¡El producto ha sido editado!";

                return RedirectToAction("Index");
            }
            return View(product);
        }
        //GET Admin/Product/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(context.categories.OrderBy(x => x.Clasificación), "Id", "Nombre", product.CategoryId);

            return View(product);
        }

        //POST Admin/Product/Editar

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            ViewBag.CategoryId = new SelectList(context.categories.OrderBy(x => x.Clasificación), "Id", "Nombre", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Nombre.ToLower().Replace(" ", "-");

                var slug = await context.Products.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == product.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "¡El producto ya existe!");
                    return View(product);

                }



                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/product");
                    if (!string.Equals(product.Imagen, "niimagen.png"))
                    {
                        string oldImagenPath = Path.Combine(uploadsDir, product.Imagen);
                        if (System.IO.File.Exists(oldImagenPath))
                        {
                            System.IO.File.Delete(oldImagenPath);
                        }
                    }
                    string imagenNombre = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imagenNombre);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();



                    product.Imagen = imagenNombre;
                }
                    context.Update(product);
                    await context.SaveChangesAsync();

                    TempData["Success"] = "¡El producto ha sido editado!";

                    return RedirectToAction("Index");
                }
                return View(product);
            }

        //GET Admin/Pages/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await context.Products.FindAsync(id);
            if (product== null)
            {

                TempData["Error"] = "¡La página no existe!";

            }

            else
            {
                if (!string.Equals(product.Imagen, "niimagen.png"))
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/product");
                    string oldImagenPath = Path.Combine(uploadsDir, product.Imagen);
                    if (System.IO.File.Exists(oldImagenPath))
                    {
                        System.IO.File.Delete(oldImagenPath);
                    }
                }
                context.Products.Remove(product);
                await context.SaveChangesAsync();

                TempData["Success"] = "¡El producto ha sido eliminada!";
            }
            return RedirectToAction("Index");
        }

    }
    }
