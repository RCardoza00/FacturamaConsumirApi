using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApiFacturamaTest.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace FacturamaConsumirApi.Controllers
{
	public class HomeController : Controller
	{
		public async  Task<ActionResult> Index()
		{
			string servicio = "https://localhost:44323/api/cdfi";
			var httpClient = new HttpClient();
			var json = await httpClient.GetStringAsync(servicio);
			var listaFacturas = JsonConvert.DeserializeObject<List<FacturaModel>>(json);
			
			return View(listaFacturas);
		}

		public async Task<ActionResult> folioFiscalini(string folioFiscal)
		{
			string servicio = "https://localhost:44323/api/CdfiByFolio/"+ folioFiscal;
			var httpClient = new HttpClient();
			var json = await httpClient.GetStringAsync(servicio);
			var listaFacturas = JsonConvert.DeserializeObject<FacturaModel>(json);

			return View(listaFacturas);
			//Response.Write("<script>alert('" + folioFiscal + "')</script>");

		}
		public async Task<ActionResult> FacturasFolio(string FolioFiscal)
		{

			string servicio = "https://localhost:44323/api/CdfiByFolio/"+ FolioFiscal;
			//Response.Write("<script>alert('" + servicio + "')</script>");
			var httpClient = new HttpClient();
			var json = await httpClient.GetStringAsync(servicio);
			var factura = JsonConvert.DeserializeObject<FacturaModel>(json);
			List<FacturaModel> listaFacturas = new List<FacturaModel> { factura };
		

			return View ("Index", listaFacturas);

		}
		public async Task<ActionResult> FacturasMultiFiltro(string fechaInicio,string fechaFin)
		{

			string servicio = "https://localhost:44323/api/CdfiMultiFiltro/?fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin;
			//Response.Write("<script>alert('" + fechaInicio+fechaFin + "')</script>");
			var httpClient = new HttpClient();
			var json = await httpClient.GetStringAsync(servicio);
			var listaFacturas = JsonConvert.DeserializeObject<List<FacturaModel>>(json);
			return View("Index",listaFacturas);
			
		}











	}
}