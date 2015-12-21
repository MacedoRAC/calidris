using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace FirstREST.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Show()
        {
            return View();
        }

        public ActionResult Index(Lib_Primavera.Model.Func func)
        {
            
           return RedirectToAction("Show", "Picking");
        }
    }
}
