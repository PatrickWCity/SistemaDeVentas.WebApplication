using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class DetalleVentaController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();

        [HttpGet]
        public ActionResult Index()
        {
            List<DetalleVenta> resp = db_context.DetalleVenta.ToList();
            return View(resp);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearDetalleVenta(DetalleVenta detalleVenta)
        {
            db_context.DetalleVenta.Add(detalleVenta);
            db_context.SaveChanges();
            ViewBag.Msg = "DetalleVenta insertado con éxito";
            return RedirectToAction("Index", "DetalleVenta", new { Msg = "Dato agregado con éxito" });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            DetalleVenta detalleVenta = db_context.DetalleVenta.Where(x => x.idDetalle == id).FirstOrDefault();//change db
            return View(detalleVenta);
        }

        [HttpPost]
        public ActionResult EditardetalleVenta(DetalleVenta detalleVenta)
        {
            DetalleVenta estedetalleVenta = (from detalleVentas in db_context.DetalleVenta
                                     where detalleVentas.idDetalle == detalleVenta.idDetalle
                                     select detalleVentas).FirstOrDefault();
            estedetalleVenta.subTotal = detalleVenta.subTotal;
            db_context.SaveChanges();

            return RedirectToAction("Index", "DetalleVenta", new { Msg = "Dato editado con éxito" });
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            DetalleVenta estedetalleVenta = db_context.DetalleVenta.Where(x => x.idDetalle == id).FirstOrDefault();
            db_context.DetalleVenta.Remove(estedetalleVenta);
            db_context.SaveChanges();

            return RedirectToAction("Index", "DetalleVenta", new { Msg = "Dato Eliminado con éxito" });
        }
        [HttpPost]
        public JsonResult AgregarDetalleVenta(int numFactura, int idVenta, int subTotal, int idProducto, int cantidad)
        {
            DetalleVenta objdetalleVenta = new DetalleVenta()
            {
                numFactura = numFactura,
                idVenta=idVenta,
                subTotal=subTotal,
                idProducto=idProducto,
                cantidad=cantidad,
            };
            db_context.DetalleVenta.Add(objdetalleVenta);
            db_context.SaveChanges();

            return Json(new { Respuesta = "Ok", Id = objdetalleVenta.idDetalle, numFactura = objdetalleVenta.numFactura, idVenta = objdetalleVenta.idVenta, subTotal = objdetalleVenta.subTotal, idProducto = objdetalleVenta.idProducto, cantidad = objdetalleVenta.cantidad });
        }
        [HttpGet]
        public Jsonp GetAllExternal()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            List<DetalleVenta> lista = db_context.DetalleVenta.ToList();
            Jsonp obj = new Jsonp(lista);
            return obj;
        }
        #region "Console CRUD"
        [HttpGet]
        public JsonResult GetAll()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            return Json(db_context.DetalleVenta.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddDetalleVenta(int numFactura, int idVenta, int subTotal, int idProducto, int cantidad)
        {
            db_context.DetalleVenta.Add(new DetalleVenta
            {
                numFactura = numFactura,
                idVenta = idVenta,
                subTotal = subTotal,
                idProducto = idProducto,
                cantidad = cantidad
            });
            db_context.SaveChanges();
            return Json(new { respuesta = "Detalle de Venta Ingresado" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteDetalleVenta(int idDetalle)
        {
            DetalleVenta esteDetalleVenta = db_context.DetalleVenta.Where(x => x.idDetalle == idDetalle).FirstOrDefault();
            db_context.DetalleVenta.Remove(esteDetalleVenta);
            db_context.SaveChanges();
            return Json(new { respuesta = "Detalle de Venta Eliminado con éxito" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EditDetalleVenta(int idDetalle, int numFactura, int idVenta, int subTotal, int idProducto, int cantidad)
        {
            DetalleVenta esteDetalleVenta = (from DetalleVentas in db_context.DetalleVenta
                                   where DetalleVentas.idDetalle == idDetalle
                                   select DetalleVentas).FirstOrDefault();
            esteDetalleVenta.numFactura = numFactura;
            esteDetalleVenta.idVenta = idVenta;
            esteDetalleVenta.subTotal = subTotal;
            esteDetalleVenta.idProducto = idProducto;
            esteDetalleVenta.cantidad = cantidad;
            db_context.SaveChanges();
            return Json(new { respuesta = "Detalle de Venta Editado con Exito" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}