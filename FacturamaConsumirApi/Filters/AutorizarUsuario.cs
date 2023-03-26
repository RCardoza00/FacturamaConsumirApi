using MRGFE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturamaConsumirApi.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutorizarUsuario : AuthorizeAttribute
    {
        private Usuario usuario;
        private string rol;

        public AutorizarUsuario(string rol = "")
        {
            this.rol = rol;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                usuario = (Usuario)HttpContext.Current.Session["User"];
                var rolUsuario = usuario.UsuarioRol;

                if (rolUsuario != rol)
                {
                    string url = HttpContext.Current.Request.UrlReferrer.PathAndQuery.ToString();
                    filterContext.Result = new RedirectResult(url);

                }
            }
            catch (Exception ex)
            {
                string url = HttpContext.Current.Request.UrlReferrer.PathAndQuery.ToString();
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}