using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class VentaController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();

        [HttpGet]
        public ActionResult Index()
        {
            List<Venta> resp = db_context.Venta.ToList();
            return View(resp);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearVenta(Venta venta)
        {
            db_context.Venta.Add(venta);
            db_context.SaveChanges();
            ViewBag.Msg = "Venta insertado con éxito";
            return RedirectToAction("Index", "Venta", new { Msg = "Dato agregado con éxito" });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Venta venta = db_context.Venta.Where(x => x.idVenta == id).FirstOrDefault();//change db
            return View(venta);
        }

        [HttpPost]
        public ActionResult Editarventa(Venta venta)
        {
            Venta esteventa = (from ventas in db_context.Venta
                                     where ventas.idVenta == venta.idVenta
                                     select ventas).FirstOrDefault();
            esteventa.total = venta.total;// ver
            db_context.SaveChanges();

            return RedirectToAction("Index", "Venta", new { Msg = "Dato editado con éxito" });
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            Venta esteventa = db_context.Venta.Where(x => x.idVenta == id).FirstOrDefault();
            db_context.Venta.Remove(esteventa);
            db_context.SaveChanges();

            return RedirectToAction("Index", "Venta", new { Msg = "Dato Eliminado con éxito" });
        }

        [HttpPost]
        public JsonResult AgregarVenta(int total, int idCliente, int idVendedor, DateTime fecha, int descuento, int IVA)
        {
            Venta objventa = new Venta()
            {
                total =total,
                idCliente=idCliente,
                idVendedor=idVendedor,
                fecha=fecha,
                descuento=descuento,
                IVA=IVA,
            };
            db_context.Venta.Add(objventa);
            db_context.SaveChanges();

            return Json(new { Respuesta = "Ok", Id = objventa.idVenta, total = objventa.total,idCliente = objventa.idCliente, idVendedor = objventa.idVendedor, fecha = objventa.fecha, descuento = objventa.descuento, IVA = objventa.IVA });
        }
        [HttpGet]
        public Jsonp GetAllExternal()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            List<Venta> lista = db_context.Venta.ToList();
            Jsonp obj = new Jsonp(lista);
            return obj;
        }
        #region "Console CRUD"
        [HttpGet]
        public JsonResult GetAll()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            return Json(db_context.Venta.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddVenta(int total, int idCliente, int idVendedor, DateTime fecha, int? descuento, int IVA)
        {
            db_context.Venta.Add(new Venta
            {
                total = total,
                idCliente= idCliente,
                idVendedor = idVendedor,
                fecha = fecha,
                descuento=descuento,
                IVA=IVA
            });
            db_context.SaveChanges();
            return Json(new { respuesta = "Venta Ingresada" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteVenta(int idVenta)
        {
            Venta esteVenta = db_context.Venta.Where(x => x.idVenta == idVenta).FirstOrDefault();
            db_context.Venta.Remove(esteVenta);
            db_context.SaveChanges();
            return Json(new { respuesta = "Venta Eliminada con éxito" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EditVenta(int idVenta, int total, int idCliente, int idVendedor, DateTime fecha, int? descuento, int IVA)
        {
            Venta esteVenta = (from Ventas in db_context.Venta
                                             where Ventas.idVenta == idVenta
                                             select Ventas).FirstOrDefault();
            esteVenta.total = total;
            esteVenta.idCliente = idCliente;
            esteVenta.idVendedor = idVendedor;
            esteVenta.fecha = fecha;
            esteVenta.descuento = descuento;
            esteVenta.IVA = IVA;
            db_context.SaveChanges();
            return Json(new { respuesta = "Venta Editada con Exito" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}