using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class ModoPagoController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();

        [HttpGet]
        public ActionResult Index()
        {
            List<ModoPago> resp = db_context.ModoPago.ToList();
            return View(resp);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearModoPago(ModoPago modoPago)
        {
            db_context.ModoPago.Add(modoPago);
            db_context.SaveChanges();
            ViewBag.Msg = "ModoPago insertado con éxito";
            return RedirectToAction("Index", "ModoPago", new { Msg = "Dato agregado con éxito" });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ModoPago modoPago = db_context.ModoPago.Where(x => x.numPago == id).FirstOrDefault();//change db
            return View(modoPago);
        }

        [HttpPost]
        public ActionResult EditarmodoPago(ModoPago modoPago)
        {
            ModoPago estemodoPago = (from modoPagos in db_context.ModoPago
                                     where modoPagos.numPago == modoPago.numPago
                                     select modoPagos).FirstOrDefault();
            estemodoPago.nombre = modoPago.nombre;
            db_context.SaveChanges();

            return RedirectToAction("Index", "ModoPago", new { Msg = "Dato editado con éxito" });
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            ModoPago estemodoPago = db_context.ModoPago.Where(x => x.numPago == id).FirstOrDefault();
            db_context.ModoPago.Remove(estemodoPago);
            db_context.SaveChanges();

            return RedirectToAction("Index", "ModoPago", new { Msg = "Dato Eliminado con éxito" });
        }
        [HttpPost]
        public JsonResult AgregarModoPago(string nombre, string otroDetalles)
        {
            ModoPago objmodoPago = new ModoPago()
            {
                nombre = nombre,
                otroDetalles = otroDetalles,
            };
            db_context.ModoPago.Add(objmodoPago);
            db_context.SaveChanges();

            return Json(new { Respuesta = "Ok", Id = objmodoPago.numPago, nombre = objmodoPago.nombre, otroDetalles = objmodoPago.otroDetalles });
        }
        [HttpGet]
        public Jsonp GetAllExternal()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            List<ModoPago> lista = db_context.ModoPago.ToList();
            Jsonp obj = new Jsonp(lista);
            return obj;
        }
        #region "Console CRUD"
        [HttpGet]
        public JsonResult GetAll()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            return Json(db_context.ModoPago.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddModoPago(string nombre, string otroDetalles)
        {
            db_context.ModoPago.Add(new ModoPago
            {
                nombre = nombre,
                otroDetalles = otroDetalles
            });
            db_context.SaveChanges();
            return Json(new { respuesta = "Modo de Pago Ingresado" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteModoPago(int numPago)
        {
            ModoPago esteModoPago = db_context.ModoPago.Where(x => x.numPago == numPago).FirstOrDefault();
            db_context.ModoPago.Remove(esteModoPago);
            db_context.SaveChanges();
            return Json(new { respuesta = "Modo de Pago Eliminado con éxito" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EditModoPago(int numPago, string nombre, string otroDetalles)
        {
            ModoPago esteModoPago = (from ModoPagos in db_context.ModoPago
                                   where ModoPagos.numPago == numPago
                                   select ModoPagos).FirstOrDefault();
            esteModoPago.nombre = nombre;
            esteModoPago.otroDetalles = otroDetalles;
            db_context.SaveChanges();
            return Json(new { respuesta = "Modo de Pago Editado con Exito" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}