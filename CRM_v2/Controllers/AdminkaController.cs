using System;
using System.Web;
using System.Web.Mvc;

namespace CRM_v2.Controllers
{
     [Authorize(Roles = "Administrator")]
    public class AdminkaController : ContrlLoggerController
    {
        // GET: Adminka
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            //return View("Chat");
            return RedirectToAction("Index", "Chat");
        }
    }
}