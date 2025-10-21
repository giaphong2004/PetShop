using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebsiteBanHang.Business;
using WebsiteBanHang.Data;

namespace WebsiteBanHang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    string username = authTicket.Name;
                    var userService = new UserService();
                    var user = userService.GetUserByUsername(username);

                    if (user != null)
                    {
                        var roles = user.PhanQuyens.Select(pq => pq.ChucNang.TenChucNang).ToArray();
                        var principal = new GenericPrincipal(new GenericIdentity(username), roles);
                        HttpContext.Current.User = principal;

                        if (HttpContext.Current.Session != null)
                        {
                            if (HttpContext.Current.Session["FullName"] == null)
                            {
                                HttpContext.Current.Session["FullName"] = user.TenNguoiDung;
                            }
                        }
                    }
                }
            }
        }
    }
}
