using FacturamaConsumirApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FacturamaConsumirApi.Controllers
{
    public class EmisorController : Controller
    {
        static HttpClient httpClient = new HttpClient();

        // GET: Emisor
        public ActionResult IndexE()
        {
            return View();
        }

        public async Task<ActionResult> Index()
        {
            string servicio = "https://localhost:44370/api/emisor";
            var json = await httpClient.GetStringAsync(servicio);
            var listaEmisores = JsonConvert.DeserializeObject<List<EmisorModel>>(json);

            return View(listaEmisores);
        }

        // GET: Emisor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Emisor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emisor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Emisor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Emisor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Emisor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Emisor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
