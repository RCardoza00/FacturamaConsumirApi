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
using MRGFE.Models;

namespace FacturamaConsumirApi.Controllers
{
	public class HomeController : Controller
	{
		string Baseurl = "http://54.203.169.36/MRGFE/";
		static HttpClient httpClient = new HttpClient();

        public async  Task<ActionResult> Index()
		{
			string servicio = "http://54.203.169.36/MRGFE/api/cfdi";
			var json = await httpClient.GetStringAsync(servicio);
			var listaFacturas = JsonConvert.DeserializeObject<List<CFDI>>(json);
			
			return View(listaFacturas);
		}

		
		public async Task<ActionResult> FacturasFolio(string FolioFiscal)
		{
			//string servicio = $"https://localhost:44323/api/CdfiByFolio/{FolioFiscal}";
			// List<CFDI> listaFacturas = new List<CFDI>();
			List<CFDI> Facturita=new List<CFDI>();
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
					var factura = JsonConvert.DeserializeObject<CFDI>(CfdiResponse);
					Facturita = new List<CFDI> { factura };
				}
				return View("Index", Facturita);

			}
			//var json = await httpClient.GetStringAsync(servicio);
			//Exception exception = Server.GetLastError();

			//HttpException httpException = exception as HttpException;

			//var factura = JsonConvert.DeserializeObject<CFDI>(json);
		}
		public async Task<ActionResult> FacturasMultiFiltro(string fechaInicio, string fechaFin, string rfcEmisor, string rfcReceptor, string serie, string folio)
		{
            string servicio = $"http://54.203.169.36/MRGFE/api/cfdi/filtrar/?fechaInicio={fechaInicio}&fechaFin={fechaFin}&rfcEmisor={rfcEmisor}&rfcReceptor={rfcReceptor}&serie={serie}&folio={folio}";
            //Response.Write("<script>alert('" + servicio + "')</script>");
            List<CFDI> EmpInfo = new List<CFDI>();
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
                    EmpInfo = JsonConvert.DeserializeObject<List<CFDI>>(EmpResponse);
                }
                //returning the employee list to view
                return View("Index", EmpInfo);

            }
        }

        [HttpPost]
		public ActionResult DownloadCfdiZip(List<string> fls, List<string> xmls, List<string> pdfs)
		{
            if (fls != null && fls.Count() > 0 && 
                xmls != null && xmls.Count() > 0  && 
                pdfs != null && pdfs.Count() > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    {
                        for (int i = 0; i < fls.Count(); i++)
                        {
                            var entryPdf = zip.CreateEntry($"{fls[i]}.pdf");
                            using (var fileStreamPdf = new MemoryStream(Convert.FromBase64String(pdfs[i])))
                            {
                                using (var entryStream = entryPdf.Open())
                                {
                                    fileStreamPdf.CopyTo(entryStream);
                                }
                            }
                            var entryXml = zip.CreateEntry($"{fls[i]}.xml");
                            using (var fileStreamXml = new MemoryStream(Convert.FromBase64String(xmls[i])))
                            {
                                using (var entryStream = entryXml.Open())
                                {
                                    fileStreamXml.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                    return File(ms.ToArray(), "application/zip", "CFDIs.zip");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
	}
}