using SINF_EXAMPLE_WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class ArtigoPendenteController : ApiController
    {
        //
        // GET: /Clientes/

        public IEnumerable<ArtigoPendente> Get()
        {
            return IntegrationPri.ListaArtigosPendentes();
        }

    }
}
