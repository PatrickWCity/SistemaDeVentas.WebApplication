using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ComprarController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();
        //
        // GET: /Comprar/
        public ActionResult Index()
        {

            List<ModoPago> resp = db_context.ModoPago.ToList();
            return View(resp);
        }
        [HttpGet]
        public ActionResult Comprar(DetalleVenta detalleVenta, Venta venta, Factura factura)
        {
            //revisar
            db_context.DetalleVenta.Add(detalleVenta);
            db_context.Venta.Add(venta);
            db_context.Factura.Add(factura);
            db_context.SaveChanges();
            ViewBag.Msg = "La compra fue hecha con exito!";
            return RedirectToAction("Index", "Compras", new { Msg = "La compra fue hecha con exito!" });
        }

    }
}
