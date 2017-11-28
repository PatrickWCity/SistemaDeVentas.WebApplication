using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ComprasController : BeforeInController
    {
        //
        // GET: /Compras/

        public ActionResult Index()
        {
            return View();
        }

    }
}
