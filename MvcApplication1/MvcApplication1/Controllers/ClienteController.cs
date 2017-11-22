using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class ClienteController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();

        [HttpGet]
        public ActionResult Index()
        {
            List<Cliente> resp = db_context.Cliente.ToList();
            return View(resp);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearCliente(Cliente cliente)
        {
            db_context.Cliente.Add(cliente);
            db_context.SaveChanges();
            ViewBag.Msg = "Cliente insertado con éxito";
            return RedirectToAction("Index", "Cliente", new { Msg = "Dato agregado con éxito" });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Cliente cliente = db_context.Cliente.Where(x => x.idCliente == id).FirstOrDefault();//change db
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Editarcliente(Cliente cliente)
        {
            Cliente estecliente = (from clientes in db_context.Cliente
                                     where clientes.idCliente == cliente.idCliente
                                     select clientes).FirstOrDefault();
            estecliente.nombre = cliente.nombre;
            db_context.SaveChanges();

            return RedirectToAction("Index", "Cliente", new { Msg = "Dato editado con éxito" });
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            Cliente estecliente = db_context.Cliente.Where(x => x.idCliente == id).FirstOrDefault();
            db_context.Cliente.Remove(estecliente);
            db_context.SaveChanges();

            return RedirectToAction("Index", "Cliente", new { Msg = "Dato Eliminado con éxito" });
        }
        [HttpPost]
        public JsonResult AgregarCliente(string nombre, string apPaterno, string apMaterno, string direccion, string telefono, string rut)
        {
            Cliente objcliente = new Cliente()
            {
                nombre = nombre,
                apPaterno = apPaterno,
                apMaterno = apMaterno,
                direccion = direccion,
                telefono=telefono,
                rut=rut,
            };
            db_context.Cliente.Add(objcliente);
            db_context.SaveChanges();

            return Json(new { Respuesta = "Ok", Id = objcliente.idCliente, nombre = objcliente.nombre, apPaterno = objcliente.apPaterno, apMaterno = objcliente.apMaterno, direccion = objcliente.direccion, telefono = objcliente.telefono, rut = objcliente.rut });
        }
        [HttpGet]
        public Jsonp GetAllExternal()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            List<Cliente> lista = db_context.Cliente.ToList();
            Jsonp obj = new Jsonp(lista);
            return obj;
        }
        #region "Console CRUD"
        [HttpGet]
        public JsonResult GetAll()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            return Json(db_context.Cliente.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddCliente(string nombre, string apPaterno, string apMaterno, string direccion, string telefono, string rut)
        {
            db_context.Cliente.Add(new Cliente
            {
                nombre = nombre,
                apPaterno = apPaterno,
                apMaterno = apMaterno,
                direccion = direccion,
                telefono = telefono,
                rut = rut
            });
            db_context.SaveChanges();
            return Json(new { respuesta = "Cliente Ingresado" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteCliente(int idCliente)
        {
            Cliente esteCliente = db_context.Cliente.Where(x => x.idCliente == idCliente).FirstOrDefault();
            db_context.Cliente.Remove(esteCliente);
            db_context.SaveChanges();
            return Json(new { respuesta = "Cliente Eliminado con éxito" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EditCliente(int idCliente, string nombre, string apPaterno, string apMaterno, string direccion, string telefono, string rut)
        {
            Cliente esteCliente = (from Clientes in db_context.Cliente
                                       where Clientes.idCliente == idCliente
                                       select Clientes).FirstOrDefault();
            esteCliente.nombre = nombre;
            esteCliente.apPaterno = apPaterno;
            esteCliente.apMaterno = apMaterno;
            esteCliente.direccion = direccion;
            esteCliente.telefono = telefono;
            esteCliente.rut = rut;
            db_context.SaveChanges();
            return Json(new { respuesta = "Cliente Editado con Exito" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}