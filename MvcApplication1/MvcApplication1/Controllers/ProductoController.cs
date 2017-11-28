using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class ProductoController : Controller
    {
        private SistemaVentaEntities db_context = new SistemaVentaEntities();
        #region "ASP Clase inciales"
        [HttpGet]
        public ActionResult Index()//asp get all
        {
            List<Producto> resp = db_context.Producto.ToList();
            return View(resp);

            //List<Producto> resp = db_context.Producto.OrderByDescending(q => q.idProducto).Take(10).ToList();
            //return View(resp);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearProducto(Producto producto)
        {
            db_context.Producto.Add(producto);
            db_context.SaveChanges();
            ViewBag.Msg = "Producto insertado con éxito";
            return RedirectToAction("Index", "Producto", new { Msg = "Dato agregado con éxito" });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Producto producto = db_context.Producto.Where(x => x.idProducto == id).FirstOrDefault();//change db
            return View(producto);
        }
        [HttpGet]
        public ActionResult Ver(int id)//ver producto
        {
            Producto producto = db_context.Producto.Where(x => x.idProducto == id).FirstOrDefault();//change db
            return View(producto);
        }
        [HttpPost]
        public ActionResult Editarproducto(Producto producto)
        {
            Producto esteproducto = (from productos in db_context.Producto
                               where productos.idProducto == producto.idProducto
                               select productos).FirstOrDefault();
            esteproducto.nombre = producto.nombre;
            esteproducto.descripcion = producto.descripcion;
            esteproducto.precioUnitario = producto.precioUnitario;
            esteproducto.url_imagen = producto.url_imagen;
            esteproducto.idCategoria = producto.idCategoria;
            db_context.SaveChanges();

            return RedirectToAction("Index", "Producto", new { Msg = "Dato editado con éxito" });
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            Producto esteproducto = db_context.Producto.Where(x => x.idProducto == id).FirstOrDefault();
            db_context.Producto.Remove(esteproducto);
            db_context.SaveChanges();

            return RedirectToAction("Index", "Producto", new { Msg = "Dato Eliminado con éxito" });
        }

        [HttpPost]
        public JsonResult AgregarProducto(string nombre, int precioUnitario, string url_imagen,int idCategoria)
        {
            Producto objproducto = new Producto()
            {
                nombre = nombre,
                precioUnitario = precioUnitario,
                url_imagen = url_imagen,
                idCategoria = idCategoria,
            };
            db_context.Producto.Add(objproducto);
            db_context.SaveChanges();

            return Json(new { Respuesta = "Ok", Id = objproducto.idProducto, nombre = objproducto.nombre, precioUnitario = objproducto.precioUnitario, url_imagen = objproducto.url_imagen, idCategoria=objproducto.idCategoria  });
        }
        #endregion
        #region "HTML External GetAll"
        [HttpGet]
        public Jsonp GetAllExternal()//html get all
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            List<Producto> lista = db_context.Producto.ToList();
            Jsonp obj = new Jsonp(lista);
            return obj;
        }
        #endregion
        #region "Console CRUD"
        [HttpGet]
        public JsonResult GetAll()
        {
            db_context.Configuration.ProxyCreationEnabled = false;
            return Json(db_context.Producto.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddProducto(string nombre, string descripcion, int precioUnitario, string url_imagen, int idCategoria)
        {
            db_context.Producto.Add(new Producto {
                nombre = nombre,
                descripcion = descripcion,
                precioUnitario = precioUnitario,
                url_imagen = url_imagen,
                idCategoria = idCategoria
            });
            db_context.SaveChanges();
            return Json(new { respuesta = "Producto Ingresado" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteProducto(int idProducto)
        {
            Producto esteproducto = db_context.Producto.Where(x => x.idProducto == idProducto).FirstOrDefault();
            db_context.Producto.Remove(esteproducto);
            db_context.SaveChanges();
            return Json(new { respuesta = "Producto Eliminado con éxito" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EditProducto(int idProducto, string nombre, string descripcion, int precioUnitario, string url_imagen, int idCategoria)
        {
            Producto esteproducto = (from productos in db_context.Producto
                                     where productos.idProducto == idProducto
                                     select productos).FirstOrDefault();
            esteproducto.nombre = nombre;
            esteproducto.descripcion = descripcion;
            esteproducto.precioUnitario = precioUnitario;
            esteproducto.url_imagen = url_imagen;
            esteproducto.idCategoria = idCategoria;
            db_context.SaveChanges();
            return Json(new { respuesta = "Producto Editado con Exito" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}