using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artezanias2.Infrastructure;
using Artezanias2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Artezanias2.Controllers
{
    public class CartController : Controller
    {
        private readonly Artezanias2Context context;


        public CartController(Artezanias2Context context)
        {
            this.context = context;
        }
        //get /cart
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Precio * x.Cantidad)

            };



            return View(cartVM);
        }

        //get /cart/add/5
        public async Task<IActionResult> Add(int id)
        {

            Product product = await context.Products.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem== null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Cantidad += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            if (HttpContext.Request.Headers["X-Requested-With"] !="XMLHttpRequest")

            return RedirectToAction("Index");

            return ViewComponent("SmallCart");
        }
        //get /cart/decrease/5
        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Cantidad>1)
            {
                --cartItem.Cantidad;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }
        
            if (cart.Count == 0)

            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            
            return RedirectToAction("Index");
        } 
        //get /remove //5
        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
         
            
                cart.RemoveAll(x => x.ProductId == id);
            

            if (cart.Count == 0)

            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }


            return RedirectToAction("Index");
        }
        //get /cart/clear //5
        public IActionResult Clear()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            
                HttpContext.Session.Remove("Cart");



            // return RedirectToAction("Pages", "Pages");
            // return Redirect("/");
            if(HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            return Redirect(Request.Headers["Referer"].ToString());

            return Ok();
        }

    }
}