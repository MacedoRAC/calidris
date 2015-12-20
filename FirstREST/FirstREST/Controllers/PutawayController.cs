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
    public class PutawayController : Controller
    {
        public async Task<ActionResult> Index(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49822/api/DocCompra/" + id);

            var encomenda = (DocCompra)await response.Content.ReadAsAsync<DocCompra>();

            if (encomenda == null)
            {
                return RedirectToAction("Show");
            }


            return View(encomenda);
        }


        public async Task<ActionResult> Show()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49822/api/DocCompra/");


            var encomendas = (List<DocCompra>)await response.Content.ReadAsAsync<List<DocCompra>>();


            return View(encomendas);
        }

    }
}
