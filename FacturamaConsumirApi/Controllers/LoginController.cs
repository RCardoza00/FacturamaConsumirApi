using MRGFE.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

namespace FacturamaConsumirApi.Controllers
{
    public class LoginController : Controller
    {

        string Baseurl = "https://localhost:44370/";
        static HttpClient httpClient = new HttpClient();
        // GET: Login
        public ActionResult Sign_In()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LogInUser() {
            string servicio = $"{Baseurl}autenticar/?correo={Request.Params["correo"]}&password={Request.Params["password"]}";
            // string content = $"{{\"UsuarioCorreo\": \"{Request.Params["correo"]}\"\n\"UsuarioPassword\": \"{Request.Params["password"]}\"}}";
            
            HttpResponseMessage res = await httpClient.GetAsync(servicio);
            Usuario usr = new Usuario();
            if (res.IsSuccessStatusCode)
            {
                var usrResponse = res.Content.ReadAsStringAsync().Result;
                var usrlst = JsonConvert.DeserializeObject<List<Usuario>>(usrResponse.ToString());
                usr = usrlst[0];

                Session["User"] = usr;
            }

            if (usr.UsuarioNombre == null) {
                return Redirect("Sign_In");
            }
            /*
            List<Claim> clms = new List<Claim>();
            clms.Add(new Claim(ClaimTypes.Name, usr.UsuarioNombre));
            clms.Add(new Claim(ClaimTypes.Role, usr.UsuarioRol));
            clms.Add(new Claim(ClaimTypes.Email, usr.UsuarioCorreo));

            ClaimsIdentity identity = new ClaimsIdentity(clms, "ConfirmedUser");

            var cookie = new HttpCookie("ConfirmedUser");

            cookie.Value = JsonConvert.SerializeObject(identity, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling= ReferenceLoopHandling.Serialize,
            });
            cookie.Expires = DateTime.Now.AddMinutes(30); // Set the expiration time of the cookie
            Response.Cookies.Add(cookie); // Add the cookie to the response

            // Set the current user's identity to the claims identity
            var principal = new ClaimsPrincipal(identity);
            HttpContext.User = principal;
            Thread.CurrentPrincipal = principal;
            */

            var cookieText = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(usr));
            var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "ProtectCookie"));
            var cookie = new HttpCookie("ConfirmedUser");
            cookie.Value = encryptedValue;
            cookie.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Add(cookie);

            /*
             Para desencriptar el valor de la Cookie:
            var bytes = Convert.FromBase64String(Request.Cookies["{NOMBRE_DE_LA_COOKIE}"].Value);
            var output = MachineKey.Unprotect(bytes, "ProtectCookie");
            string result = Encoding.UTF8.GetString(output);
             */

            return RedirectToAction("Index", "Home");
        }
    }
}