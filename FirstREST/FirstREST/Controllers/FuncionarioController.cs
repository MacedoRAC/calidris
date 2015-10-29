using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class FuncionarioController : ApiController
    {
        //
        // GET: /api/Funcionario

        public Boolean Get(int id, String password)
        {
            Lib_Primavera.Model.Funcionario funcionario = Lib_Primavera.PriIntegration.verificaFuncionario(id, password);
            if (funcionario == null)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

    }
}
