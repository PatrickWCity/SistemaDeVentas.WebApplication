using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Index()
        {
            List<Producto> resp = db_context.Producto.OrderByDescending(q => q.idProducto).Take(4).ToList();
            return View(resp);
           // return View(db_context.Urls.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Sobre Nosotros!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contactenos!";

            return View();
        }
    }
}