using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        private SistemaVentaEntities db = new SistemaVentaEntities();
        public ActionResult Index()
        {
            return View("Cart");
        }
        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Producto.idProducto == id)
                    return i;
            return -1;
        }

        public ActionResult Eliminar(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];

            if (cart[index].Cantidad > 1)
            {
                cart[index].Cantidad--;
            }
            else
            {
                cart.RemoveAt(index);
            }

            Session["cart"] = cart;
            return View("Cart");
        }

        public ActionResult OrderNow(int id)

        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(
                    db.Producto.Find(id),1));
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new Item(db.Producto.Find(id), 1));
                else
                    cart[index].Cantidad++;
                Session["cart"] = cart;
            }
            return View("Cart");
        }
    }
}
