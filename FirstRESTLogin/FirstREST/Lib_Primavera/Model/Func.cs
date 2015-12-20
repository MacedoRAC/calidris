using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Lib_Primavera.Model
{
    public class Func : ApiController
    {
        public string username
        {
            get;
            set;
        }

        public string password
        {
            get;
            set;
        }
    }
}
