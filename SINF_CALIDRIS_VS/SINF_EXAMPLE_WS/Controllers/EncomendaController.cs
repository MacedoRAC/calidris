using SINF_EXAMPLE_WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class EncomendaController : ApiController
    {
        //
        // GET: /Encomendas/

        public IEnumerable<Encomenda> Get()
        {
            return IntegrationPri.ListaEncomendasPendentes();
        }
    }
}
