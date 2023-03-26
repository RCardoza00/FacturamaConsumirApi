using FacturamaConsumirApi.Controllers;
using MRGFE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturamaConsumirApi.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        private Usuario usuario;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                usuario = (Usuario)HttpContext.Current.Session["User"];
                if ( usuario == null)
                {
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Login/Sign_In");
                    }
                }
            }
            catch (Exception ex)
            {
                filterContext.HttpContext.Response.Redirect("/Login/Sign_In");

            }
        }
    }
}