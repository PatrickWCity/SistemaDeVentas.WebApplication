using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class VendedorController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();

        [HttpGet]
        public ActionResult Index()
        {
            List<Vendedor> resp = db_context.Vendedor.ToList();
            return View(resp);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearVendedor(Vendedor vendedor)
        {
            db_context.Vendedor.Add(vendedor);
            db_context.SaveChanges();
            ViewBag.Msg = "Vendedor insertado con éxito";
            return RedirectToAction("Index", "Vendedor", new { Msg = "Dato agregado con éxito" });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Vendedor vendedor = db_context.Vendedor.Where(x => x.idVendedor == id).FirstOrDefault();//change db
            return View(vendedor);
        }

        [HttpPost]
        public ActionResult Editarvendedor(Vendedor vendedor)
        {
            Vendedor estevendedor = (from vendedors in db_context.Vendedor
                                     where vendedors.idVendedor == vendedor.idVendedor
                                     select vendedors).FirstOrDefault();
            estevendedor.nombre = vendedor.nombre;
            estevendedor.apPaterno = vendedor.apPaterno;
            estevendedor.apMaterno = vendedor.apMaterno;
            estevendedor.telefono = vendedor.telefono;
            estevendedor.rut = vendedor.rut;
            db_context.SaveChanges();

            return RedirectToAction("Index", "Vendedor", new { Msg = "Dato editado con éxito" });
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            Vendedor estevendedor = db_context.Vendedor.Where(x => x.idVendedor == id).FirstOrDefault();
            db_context.Vendedor.Remove(estevendedor);
            db_context.SaveChanges();

            return RedirectToAction("Index", "Vendedor", new { Msg = "Dato Eliminado con éxito" });
        }

        [HttpPost]
        public JsonResult AgregarVendedor(string nombre, string apPaterno, string apMaterno, string rut, string telefono)
        {
            Vendedor objvendedor = new Vendedor()
            {
                nombre = nombre,
                apPaterno=apPaterno,
                apMaterno=apMaterno,
                rut=rut,
                telefono=telefono,
            };
            db_context.Vendedor.Add(objvendedor);
            db_context.SaveChanges();

            return Json(new { Respuesta = "Ok", Id = objvendedor.idVendedor, nombre = objvendedor.nombre, apPaterno = objvendedor.apMaterno, rut = objvendedor.rut, telefono = objvendedor.telefono });
        }
        [HttpGet]
        public Jsonp GetAllExternal()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            List<Vendedor> lista = db_context.Vendedor.ToList();
            Jsonp obj = new Jsonp(lista);
            return obj;
        }
        #region "Console CRUD"
        [HttpGet]
        public JsonResult GetAll()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            return Json(db_context.Vendedor.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddVendedor(string nombre, string apPaterno, string apMaterno, string rut, string telefono)
        {
            db_context.Vendedor.Add(new Vendedor
            {
                nombre = nombre,
                apPaterno = apPaterno,
                apMaterno = apMaterno,
                telefono = telefono,
                rut = rut
            });
            db_context.SaveChanges();
            return Json(new { respuesta = "Vendedor Ingresado" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteVendedor(int idVendedor)
        {
            Vendedor esteVendedor = db_context.Vendedor.Where(x => x.idVendedor == idVendedor).FirstOrDefault();
            db_context.Vendedor.Remove(esteVendedor);
            db_context.SaveChanges();
            return Json(new { respuesta = "Vendedor Eliminado con éxito" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EditVendedor(int idVendedor, string nombre, string apPaterno, string apMaterno, string telefono, string rut)
        {
            Vendedor esteVendedor = (from Vendedors in db_context.Vendedor
                                   where Vendedors.idVendedor == idVendedor
                                   select Vendedors).FirstOrDefault();
            esteVendedor.nombre = nombre;
            esteVendedor.apPaterno = apPaterno;
            esteVendedor.apMaterno = apMaterno;
            esteVendedor.telefono = telefono;
            esteVendedor.rut = rut;
            db_context.SaveChanges();
            return Json(new { respuesta = "Vendedor Editado con Exito" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}