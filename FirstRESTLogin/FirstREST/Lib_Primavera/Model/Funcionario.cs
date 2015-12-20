using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Lib_Primavera.Model
{
    public class Funcionario : Controller
    {
        public int CDU_id
        {
            get;
            set;
        }

        public String CDU_Nome
        {
            get;
            set;
        }

        public String CDU_Password
        {
            get;
            set;
        }

    }
}
