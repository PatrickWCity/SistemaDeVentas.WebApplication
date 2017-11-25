using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BeforeInController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Usuarios usuariologueado =(Usuarios) Session["usuario"];
            //var userobj = db.Usuarios.Where(z => z.email == usuario && z.password == contrasena).FirstOrDefault();
            if (usuariologueado == null)
            {
                filterContext.Result = new RedirectResult("/Login/index");
            }
            else
            
            {           
                string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string action = filterContext.ActionDescriptor.ActionName;
                bool flag = false;
                foreach (var accesos in usuariologueado.Accesos_usuarios)
                {
                    if (action == accesos.Urls.action && controller == accesos.Urls.controller)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    filterContext.Result = new RedirectResult("/Login/index");
                }

            }

        }
    }
}
