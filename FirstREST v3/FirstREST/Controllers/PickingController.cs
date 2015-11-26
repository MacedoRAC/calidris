using FirstREST.Lib_Primavera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Controllers
{
    public class PickingController : Controller
    {
        public async Task<ActionResult> Encomenda(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49822/api/DocVenda/" + id);

            DocVenda encomenda = await response.Content.ReadAsAsync<DocVenda>();

            ViewData["ref"] = id;

            return View(encomenda);
        }
    }
}
