using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class RegistrarController : Controller
    {
        //
        // GET: /Registrar/
        private SistemaVentaEntities db_context = new SistemaVentaEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearUsuario(Usuarios usuario)
        {
            db_context.Usuarios.Add(usuario);
            db_context.SaveChanges();
            ViewBag.Msg = "Cuenta Creada con éxito";
            return RedirectToAction("Index", "Login", new { Msg = "Cuenta Creada con éxito" });
        }

    }
}
