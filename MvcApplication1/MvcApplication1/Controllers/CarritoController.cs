using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class CarritoController : Controller
    {
        SistemaVentaEntities db = new SistemaVentaEntities();
        [HttpPost]
        public JsonResult AddProductoCarro(int id)
        {
            Producto buscar = db.Producto.Where(x => x.idProducto == id).FirstOrDefault();
            if(buscar != null)
            {
                if (Session["Carrito"] == null)
                {
                    List<ProductoCarro> list = new List<ProductoCarro>();
                    list.Add(new ProductoCarro { Id = buscar.idProducto, Nombre = buscar.nombre, Precio = buscar.precioUnitario, Cantidad=1 });
                    Session["Carrito"] = list;
                }
                else
                {
                    List<ProductoCarro> list = (List<ProductoCarro>)Session["Carrito"];
                    ProductoCarro buscarencarro = list.Where(x => x.Id == buscar.idProducto).FirstOrDefault();
                    if (buscarencarro == null)
                    {
                        list.Add(new ProductoCarro { Id = buscar.idProducto, Nombre = buscar.nombre, Precio = buscar.precioUnitario, Cantidad = 1 });
                    }
                    else
                    {
                        //?
                    }
                }
            }
            return Json(new { Respuesta = "Ok", Id = buscar.idProducto, Nombre = buscar.nombre, Precio = buscar.precioUnitario, Cantidad = 1 });
        }
        // GET: Carrito
        public ActionResult Index()
        {
            List<ProductoCarro> list = (List<ProductoCarro>)Session["Carrito"];
            return View(list);

        }
    }
}