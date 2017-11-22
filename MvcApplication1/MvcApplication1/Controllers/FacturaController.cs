using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class FacturaController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();

        [HttpGet]
        public ActionResult Index()
        {
            List<Factura> resp = db_context.Factura.ToList();
            return View(resp);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearFactura(Factura factura)
        {
            db_context.Factura.Add(factura);
            db_context.SaveChanges();
            ViewBag.Msg = "Factura insertado con éxito";
            return RedirectToAction("Index", "Factura", new { Msg = "Dato agregado con éxito" });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Factura factura = db_context.Factura.Where(x => x.numFactura == id).FirstOrDefault();//change db
            return View(factura);
        }

        [HttpPost]
        public ActionResult Editarfactura(Factura factura)
        {
            Factura estefactura = (from facturas in db_context.Factura
                                     where facturas.numFactura == factura.numFactura
                                     select facturas).FirstOrDefault();
            estefactura.total = factura.total;
            db_context.SaveChanges();

            return RedirectToAction("Index", "Factura", new { Msg = "Dato editado con éxito" });
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            Factura estefactura = db_context.Factura.Where(x => x.numFactura == id).FirstOrDefault();
            db_context.Factura.Remove(estefactura);
            db_context.SaveChanges();

            return RedirectToAction("Index", "Factura", new { Msg = "Dato Eliminado con éxito" });
        }
        [HttpPost]
        public JsonResult AgregarFactura(DateTime fecha, int IVA, int total, int numPago, int descuento)
        {
            Factura objfactura = new Factura()
            {
                fecha = fecha,
                IVA = IVA,
                total = total,
                numPago = numPago,
                descuento = descuento,
            };
            db_context.Factura.Add(objfactura);
            db_context.SaveChanges();

            return Json(new { Respuesta = "Ok", Id = objfactura.numFactura, fecha = objfactura.fecha, IVA = objfactura.IVA, total = objfactura.total, numPago = objfactura.numPago, descuento = objfactura.descuento });
        }
        [HttpGet]
        public Jsonp GetAllExternal()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            List<Factura> lista = db_context.Factura.ToList();
            Jsonp obj = new Jsonp(lista);
            return obj;
        }
        #region "Console CRUD"
        [HttpGet]
        public JsonResult GetAll()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            return Json(db_context.Factura.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddFactura(DateTime fecha, int IVA, int total, int numPago, int? descuento)
        {
            db_context.Factura.Add(new Factura
            {
                fecha = fecha,
                IVA = IVA,
                total = total,
                numPago = numPago,
                descuento = descuento
            });
            db_context.SaveChanges();
            return Json(new { respuesta = "Factura Ingresada" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFactura(int numFactura)
        {
            Factura esteFactura = db_context.Factura.Where(x => x.numFactura == numFactura).FirstOrDefault();
            db_context.Factura.Remove(esteFactura);
            db_context.SaveChanges();
            return Json(new { respuesta = "Factura Eliminada con éxito" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EditFactura(int numFactura, DateTime fecha, int IVA, int total, int numPago, int? descuento)
        {
            Factura esteFactura = (from Facturas in db_context.Factura
                                   where Facturas.numFactura == numFactura
                                   select Facturas).FirstOrDefault();
            esteFactura.fecha = fecha;
            esteFactura.IVA = IVA;
            esteFactura.total = total;
            esteFactura.numPago = numPago;
            esteFactura.descuento = descuento;
            db_context.SaveChanges();
            return Json(new { respuesta = "Factura EditadA con Exito" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}