using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using CRM_v2.Models;
using CRM_v2.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CRM_v2.Controllers
{
    [Logger]
    [Authorize(Roles = "Administrator, Staff,Client")]
    public abstract class ContrlLoggerController : Controller
    {
        private UsrStatStore usrststr = new UsrStatStore();
        
        public ActionResult Logoff()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            usrststr.WriteStat(User.Identity.GetUserId(), "Logout processing");
            authenticationManager.SignOut();
            return RedirectToRoute("Mylogout");
        }

    }
}