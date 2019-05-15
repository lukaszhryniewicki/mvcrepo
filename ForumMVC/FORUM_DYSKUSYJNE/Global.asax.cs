using FluentValidation.Mvc;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;


namespace FORUM_DYSKUSYJNE
{
    public class MvcApplication : HttpApplication
    {
		

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			FluentValidationModelValidatorProvider.Configure();

			Bootstrapper.Initialise();
			ViewEngines.Engines.Clear();
	
			ViewEngines.Engines.Add(new RazorViewEngine()
			{
				ViewLocationFormats = new string[]{
			"~/Web/Views/{1}/{0}.cshtml",
			"~/Web/Views/{0}.cshtml"
			}
			});


		}

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["Cookie1"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

                principal.UserId = serializeModel.UserId;
                principal.Username = serializeModel.Username;
                principal.Roles = serializeModel.RoleName.ToArray<string>();

                HttpContext.Current.User = principal;
            }
        }
    }
}
