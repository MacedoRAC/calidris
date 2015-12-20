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

        public string Index(string username, string password)
        {
            if (!Lib_Primavera.PriIntegration.Login(username, password))
                throw new HttpResponseException(new HttpResponseMessage());

            var tokenContents = Encoding.UTF8.GetBytes(username + ":" + password);
            return Convert.ToBase64String(tokenContents);
        }
    }
}
