using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturamaConsumirApi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Sign_In()
        {
            return View();
        }
    }
}