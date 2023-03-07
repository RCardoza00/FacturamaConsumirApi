using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ApiFacturamaTest.Models;
using Newtonsoft.Json;

namespace FacturamaConsumirApi.Controllers
{
	public class HomeController : Controller
	{
		public async  Task<ActionResult> Index()
		{
			string servicio = "https://localhost:44323/api/cdfi/";
			var httpClient = new HttpClient();
			var json = await httpClient.GetStringAsync(servicio);
			var listaFacturas = JsonConvert.DeserializeObject<List<FacturaModel>>(json);
			return View(listaFacturas);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}