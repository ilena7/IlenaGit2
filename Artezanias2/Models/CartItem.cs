using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artezanias2.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductNombre { get; set; }
        public int Cantidad { get; set; }

        public decimal Precio{ get; set; }

        public decimal Total { get {return Cantidad * Precio ; } }

        public string Imagen { get; set; }

        public CartItem()
        {

        }
        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductNombre = product.Nombre;
            Precio = product.Precio;
            Cantidad = 1;
            Imagen = product.Imagen;
        }

    }
}
    