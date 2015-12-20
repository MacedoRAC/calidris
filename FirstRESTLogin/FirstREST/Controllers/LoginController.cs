using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class LoginController : ApiController
    {

        public string Login(string username, string password)
        {
            if (!Lib_Primavera.PriIntegration.Login(username, password))
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized));

            var tokenContents = Encoding.UTF8.GetBytes(username + ":" + password);
            return Convert.ToBase64String(tokenContents);
        }
    }
}
