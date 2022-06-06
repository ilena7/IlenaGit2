using Artezanias2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Infrastructure
{
    public class SmallCartViewcomponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            SmallCartViewModel smallCartVM;
            if (cart == null || cart.Count == 0)
            {
                smallCartVM = null;

            }
            else
            {
                smallCartVM = new SmallCartViewModel
                {
                    NumberOfItems = cart.Sum(x => x.Cantidad),
                    TotalAmount = cart.Sum(x => x.Cantidad * x.Precio)
                };
            }

            return View(smallCartVM);
        }
    }
}
