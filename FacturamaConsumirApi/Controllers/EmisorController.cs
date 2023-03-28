using FacturamaConsumirApi.Filters;
using FacturamaConsumirApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace FacturamaConsumirApi.Controllers
{
    public class EmisorController : Controller
    {
        string Baseurl = "http://54.203.169.36/";
        static HttpClient httpClient = new HttpClient();
        private static List<EmisorModel> le = new List<EmisorModel>();

        [AutorizarUsuario(rol: "ADMINISTRADOR")]
        // GET: Emisor
        public async Task<ActionResult> Index()
        {
            string servicio = "http://54.203.169.36/api/emisor";
            var json = await httpClient.GetStringAsync(servicio);
            var listaEmisores = JsonConvert.DeserializeObject<List<EmisorModel>>(json);

            return View(listaEmisores);
        }

        [AutorizarUsuario(rol: "ADMINISTRADOR")]
        // GET: Emisor/Details/5
        public async Task<ActionResult> Details(string id)
        {
            EmisorModel emisor = new EmisorModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/emisor/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmisorResponse = Res.Content.ReadAsStringAsync().Result;
                    emisor = JsonConvert.DeserializeObject<EmisorModel>(EmisorResponse);
                }
                return PartialView("Details", emisor);
            }
        }

        // GET: Emisor/EmisorRfc/rfc
        [HttpGet]
        public async Task<ActionResult> EmisorRfc(string EmisorRfc)
        {
            if (!EmisorRfc.IsEmpty())
            {
                List<EmisorModel> emisorEncontrado = new List<EmisorModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/emisor/" + EmisorRfc);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmisorResponse = Res.Content.ReadAsStringAsync().Result;
                        var emisor = JsonConvert.DeserializeObject<EmisorModel>(EmisorResponse);
                        emisorEncontrado = new List<EmisorModel> { emisor };
                        le = emisorEncontrado;
                    }
                    return View("Index", emisorEncontrado);

                }
            }
            return RedirectToAction("Index");
        }

        [AutorizarUsuario(rol: "ADMINISTRADOR")]
        // GET: Emisor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emisor/Create
        [HttpPost]
        public async Task<ActionResult> Create(EmisorModel emisor)
        {
            try
            {
                var _httpClient = new HttpClient();
                string servicio = "http://54.203.169.36/api/emisor";
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(servicio);

                var payload = JsonConvert.SerializeObject(emisor);

                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var result = await _httpClient.PostAsync(servicio, content);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AutorizarUsuario(rol: "ADMINISTRADOR")]
        // GET: Emisor/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            EmisorModel emisor = new EmisorModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/emisor/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmisorResponse = Res.Content.ReadAsStringAsync().Result;
                    emisor = JsonConvert.DeserializeObject<EmisorModel>(EmisorResponse);
                }
            }
            return View(emisor);
        }

        // POST: Emisor/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, EmisorModel emisor)
        {
            try
            {
                var _httpClient = new HttpClient();
                string servicio = "http://54.203.169.36/api/emisor";
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(servicio);

                var payload = JsonConvert.SerializeObject(emisor);

                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var result = await _httpClient.PutAsync(servicio, content);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AutorizarUsuario(rol: "ADMINISTRADOR")]
        // GET: Emisor/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            EmisorModel emisor = new EmisorModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/emisor/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmisorResponse = Res.Content.ReadAsStringAsync().Result;
                    emisor = JsonConvert.DeserializeObject<EmisorModel>(EmisorResponse);
                }
            }

            return View(emisor);
        }

        // POST: Emisor/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, EmisorModel emisor)
        {
            try
            {
                string rfc = emisor.EmisorRfc;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.DeleteAsync("api/emisor/" + id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
