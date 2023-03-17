using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
		string Baseurl = "http://54.203.169.36/MRGFE/";
		static HttpClient httpClient = new HttpClient();

		List<FacturaModel> lf = new List<FacturaModel>();
        public async  Task<ActionResult> Index()
		{
			string servicio = "http://54.203.169.36/MRGFE/api/cfdi";
			var json = await httpClient.GetStringAsync(servicio);
			var listaFacturas = JsonConvert.DeserializeObject<List<FacturaModel>>(json);
			
			return View(listaFacturas);
		}

		
		public async Task<ActionResult> FacturasFolio(string FolioFiscal)
		{
			//string servicio = $"https://localhost:44323/api/CdfiByFolio/{FolioFiscal}";
			List<FacturaModel> listaFacturas = new List<FacturaModel>();
			List<FacturaModel> Facturita=new List<FacturaModel>();
			//Response.Write("<script>alert('" + servicio + "')</script>");
			using (var client = new HttpClient())
			{
				//Passing service base url
				client.BaseAddress = new Uri(Baseurl);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				// Sending request to find web api REST service resource GetAllEmployees using HttpClient
				HttpResponseMessage Res = await client.GetAsync("api/cfdi/folio=" + FolioFiscal);
				if (Res.IsSuccessStatusCode)
				{
					var CfdiResponse = Res.Content.ReadAsStringAsync().Result;
					//Deserializing the response recieved from web api and storing into the Employee list
					var factura = JsonConvert.DeserializeObject<FacturaModel>(CfdiResponse);
					Facturita = new List<FacturaModel> { factura };
					lf = Facturita;
				}
				return View("Index", Facturita);

			}
			//var json = await httpClient.GetStringAsync(servicio);
			//Exception exception = Server.GetLastError();

			//HttpException httpException = exception as HttpException;

			//var factura = JsonConvert.DeserializeObject<FacturaModel>(json);
		}
		public async Task<ActionResult> FacturasMultiFiltro(string fechaInicio,string fechaFin,string rfcEmisor,string rfcReceptor)
		{
            string servicio = $"http://54.203.169.36/MRGFE/api/cfdi/filtrar/?fechaInicio={fechaInicio}&fechaFin={fechaFin}&rfcEmisor={rfcEmisor}&rfcReceptor={rfcReceptor}";
            //Response.Write("<script>alert('" + servicio + "')</script>");
            List<FacturaModel> EmpInfo = new List<FacturaModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync(servicio);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<FacturaModel>>(EmpResponse);
                    lf = EmpInfo;
                }
                //returning the employee list to view
                return View("Index", EmpInfo);

            }
        }

		public ActionResult DownloadCfdiZip()
		{
			if (lf != null && lf.Count() > 0)
			{
                using (MemoryStream ms = new MemoryStream())
                {
                    using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    {
                        foreach (var item in lf)
                        {
                            var entryPdf = zip.CreateEntry($"{item.CfdiFolioFiscal}.pdf");
                            using (var fileStreamPdf = new MemoryStream(item.CfdiIPdf))
                            {
                                using (var entryStream = entryPdf.Open())
                                {
                                    fileStreamPdf.CopyTo(entryStream);
                                }
                            }
                            var entryXml = zip.CreateEntry($"{item.CfdiFolioFiscal}.xml");
                            using (var fileStreamXml = new MemoryStream(item.CfdiIXml))
                            {
                                using (var entryStream = entryXml.Open())
                                {
                                    fileStreamXml.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                    return File(ms.ToArray(), "application/pdf", "prueba.zip");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
	}
}