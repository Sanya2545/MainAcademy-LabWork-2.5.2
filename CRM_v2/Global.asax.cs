using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using CRM_v2.Models;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CRM_v2
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // MY_ADD Create the administrator role and user.
            //string NameLiteral = ReadNameLiteral();

            RoleActions roleActions = new RoleActions();
           // roleActions.AdminName = NameLiteral;
            roleActions.createAdmin();
            roleActions.createUserClient();        
            roleActions.createStaffRole();
           
        }
        protected void Session_End(Object sender, EventArgs e)
        {
            Logger.GetInstance().Save();
            UsrStatStore usrststr = new UsrStatStore();
            Models.ApplicationDbContext cdb = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(cdb));
            var r = userManager.FindByName(Context.User.Identity.Name);
            usrststr.WriteStat(r.Id, "Session_End");
        }



        protected void Application_End(Object sender, EventArgs e)
        {
            Logger.GetInstance().SaveFin();
            UsrStatStore usrststr = new UsrStatStore();
            try
            {
               
                Models.ApplicationDbContext cdb = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(cdb));
                var r = userManager.FindByName(Context.User.Identity.Name);
                usrststr.WriteStat(r.Id, "Application_End");
            }
            catch { usrststr.WriteStat("", "Application_End"); }
        }

        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    var exception = Server.GetLastError();
        //    if (exception is HttpUnhandledException)
        //    {
        //        Server.Transfer("~/Error.aspx");
        //    }
        //    if (exception != null)
        //    {
        //        Server.Transfer("~/Error.aspx");
        //    }
        //    try
        //    {
        //        // This is to stop a problem where we were seeing "gibberish" in the
        //        // chrome and firefox browsers
        //        HttpApplication app = sender as HttpApplication;
        //        app.Response.Filter = null;
        //    }
        //    catch
        //    {
        //    }
        //}

        internal string ReadNameLiteral()
        {
           string r = "Staffadmin";            
           string rr = WebConfigurationManager.AppSettings["NameLiteral"];
           if (rr != null)
           {
               r = rr;
           }
        return r;
        }
    }
}