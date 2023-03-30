using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConfiguracionCamposMRGE.Models;
using Newtonsoft.Json;

namespace FacturamaConsumirApi.Controllers
{
    public class ConfigCamposController : Controller
    {
		string Baseurl = "http://54.203.169.36/MRGFE/";
		static HttpClient httpClient = new HttpClient();
		// GET: ConfigCampos
		public async Task<ActionResult> Index()
		{

			List<ProveedorCamposModel> camposProveedor = new List<ProveedorCamposModel>();
			List<CampoMirageModel> camposMirage = new List<CampoMirageModel>();

			try
			{
				string servicioProveedor = "http://54.203.169.36/MRGFE/api/campoproveedor";
				var jsonProveedor = await httpClient.GetStringAsync(servicioProveedor);
				camposProveedor = JsonConvert.DeserializeObject<List<ProveedorCamposModel>>(jsonProveedor);

				string servicioMirage = "http://54.203.169.36/MRGFE/api/campomirage";
				var jsonMirage = await httpClient.GetStringAsync(servicioMirage);
				camposMirage = JsonConvert.DeserializeObject<List<CampoMirageModel>>(jsonMirage);

				// Eliminar los campos que ya existen en la tabla de la vista de campos Mirage
				var camposProveedorNoAgregados = camposProveedor
					.Where(p => !camposMirage.Any(m => m.CamposMiCampo == p.CamposPrCampo))
					.ToList();

				var model = new Tuple<List<ProveedorCamposModel>, List<CampoMirageModel>>(camposProveedorNoAgregados, camposMirage);
				return View(model);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			// en caso de error, regreos una tupla vacia en la vista
			var emptyModel = new Tuple<List<ProveedorCamposModel>, List<CampoMirageModel>>(camposProveedor, camposMirage);
			return View(emptyModel);
		}

		[HttpPost]
		public async Task<ActionResult> CrearCampoProveedor(string id, string proveedor, string campo, string etiqueta, string arreglo, string tipodato, string version, string obligatorio)
		{
			// agarra los valores de los inputs para construir el objeto CampoProveedor y enviarlo como post
			//determina si es arreglo o no dependiendo de la respuesta del combobox
			byte isArreglo;
			byte isObligatorio;
			if (arreglo.Equals("Si"))
			{
				isArreglo = 1;

			}
			else
			{
				isArreglo = 0;
			}

			//determinar si el campo es obligatorio

			if (arreglo.Equals("Si"))
			{
				isObligatorio = 1;
			}
			else
			{
				isObligatorio = 0;
			}
			try
			{
				var _httpClient = new HttpClient();
				string servicio = "http://54.203.169.36/MRGFE/api/campoproveedor";
				var cliente = new HttpClient();
				cliente.BaseAddress = new Uri(servicio);
				ProveedorCamposModel campito = new ProveedorCamposModel();
				campito.CamposPrId = id;
				campito.CamposPrProveedor = proveedor;
				campito.CamposPrCampo = campo;
				campito.CamposPrEtiqueta = etiqueta;
				campito.CamposPrArreglo1 = isArreglo;
				campito.CamposPrTipoDato = tipodato;
				campito.CamposPrVersion = version;
				campito.CamposPrObliga1 = isObligatorio;
				var payload = JsonConvert.SerializeObject(campito);

				var content = new StringContent(payload, Encoding.UTF8, "application/json");

				var result = await _httpClient.PostAsync(servicio, content);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}



		}

		[HttpPost]
		public async Task<ActionResult> MoveRow(string id, string proveedor, string campo, string etiqueta, string arreglo, string tipodato, string version, string obligatorio)
		{
			// aqui agarra los parametros de la fila seleccionada para construir un objeto de tipo campoMirage
			// y enviarlo como post
			try
			{
				var _httpClient = new HttpClient();
				string servicio = "http://54.203.169.36/MRGFE/api/campomirage";
				var cliente = new HttpClient();
				cliente.BaseAddress = new Uri(servicio);

				var campoMirageModel = new CampoMirageModel();
				campoMirageModel.CamposMiId = id;
				campoMirageModel.CamposMiVersion = version;
				campoMirageModel.CamposMiEtiqueta = etiqueta;
				campoMirageModel.CamposMiObliga1 = 1;
				campoMirageModel.CamposMiArreglo1 = 1;
				campoMirageModel.CamposMiTipoDato = tipodato;
				campoMirageModel.CamposMiCampo = campo;

				var payload = JsonConvert.SerializeObject(campoMirageModel);

				var content = new StringContent(payload, Encoding.UTF8, "application/json");

				var result = await _httpClient.PostAsync(servicio, content);


				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		[HttpPost]
		public async Task<ActionResult> DeleteCampoMirage(string id)
		{
			var httpClient = new HttpClient();
			var servicio = $"{Baseurl}api/campomirage/{id}";
			var result = await httpClient.DeleteAsync(servicio);

			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<ActionResult> DeleteCampoProveedor(string id)
		{
			var httpClient = new HttpClient();
			var servicio = $"{Baseurl}api/campoproveedor/{id}";
			var result = await httpClient.DeleteAsync(servicio);

			return RedirectToAction("Index");
		}


	}
}