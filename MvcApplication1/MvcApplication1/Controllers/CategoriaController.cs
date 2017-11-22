using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class CategoriaController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();

        [HttpGet]
        public ActionResult Index()
        {
            List<Categoria> resp = db_context.Categoria.ToList();
            return View(resp);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearCategoria(Categoria categoria)
        {
            db_context.Categoria.Add(categoria);
            db_context.SaveChanges();
            ViewBag.Msg = "Categoria insertado con éxito";
            return RedirectToAction("Index", "Categoria", new { Msg = "Dato agregado con éxito" });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Categoria categoria = db_context.Categoria.Where(x => x.idCategoria == id).FirstOrDefault();//change db
            return View(categoria);
        }

        [HttpPost]
        public ActionResult Editarcategoria(Categoria categoria)
        {
            Categoria estecategoria = (from categorias in db_context.Categoria
                                     where categorias.idCategoria == categoria.idCategoria
                                     select categorias).FirstOrDefault();
            estecategoria.nombre = categoria.nombre;
            db_context.SaveChanges();

            return RedirectToAction("Index", "Categoria", new { Msg = "Dato editado con éxito" });
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            Categoria estecategoria = db_context.Categoria.Where(x => x.idCategoria == id).FirstOrDefault();
            db_context.Categoria.Remove(estecategoria);
            db_context.SaveChanges();

            return RedirectToAction("Index", "Categoria", new { Msg = "Dato Eliminado con éxito" });
        }


        [HttpPost]
        public JsonResult AgregarCategoria(string nombre, string descripcion)
        {
            Categoria objcategoria = new Categoria()
            {
                nombre = nombre,
                descripcion = descripcion,
            };
            db_context.Categoria.Add(objcategoria);
            db_context.SaveChanges();

            return Json(new { Respuesta = "Ok", Id = objcategoria.idCategoria, nombre = objcategoria.nombre, descripcion = objcategoria.descripcion });
        }
        [HttpGet]
        public Jsonp GetAllExternal()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            List<Categoria> lista = db_context.Categoria.ToList();
            Jsonp obj = new Jsonp(lista);
            return obj;
        }
        #region "Console CRUD"
        [HttpGet]
        public JsonResult GetAll()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            return Json(db_context.Categoria.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddCategoria(string nombre, string descripcion)
        {
            db_context.Categoria.Add(new Categoria
            {
                nombre = nombre,
                descripcion = descripcion
            });
            db_context.SaveChanges();
            return Json(new { respuesta = "Categoria Ingresada" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteCategoria(int idCategoria)
        {
            Categoria estecategoria = db_context.Categoria.Where(x => x.idCategoria == idCategoria).FirstOrDefault();
            db_context.Categoria.Remove(estecategoria);
            db_context.SaveChanges();
            return Json(new { respuesta = "Categoria Eliminada con éxito" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EditCategoria(int idCategoria, string nombre, string descripcion)
        {
            Categoria estecategoria = (from categorias in db_context.Categoria
                                     where categorias.idCategoria == idCategoria
                                     select categorias).FirstOrDefault();
            estecategoria.nombre = nombre;
            estecategoria.descripcion = descripcion;
            db_context.SaveChanges();
            return Json(new { respuesta = "Categoria Editada con Exito" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}