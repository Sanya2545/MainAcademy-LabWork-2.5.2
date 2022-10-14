using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRM_v2.Models;

namespace CRM_v2.Controllers
{
    public class ReInitStatisticaController : ContrlLoggerController
    {
        private CRMContext db = new CRMContext();



        // GET: ReInitStatistica
       
        
        public ActionResult Index()
        {
            //return View();
            return PartialView("_Index");
        }


         // ! ValidateAntiForgeryToken does not work Ajax.ActionLink - so jQuiery modalform.js!
       
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ReInitTbl()
         {
             try
             {
                 string sqlstr = "TRUNCATE TABLE [dbo].[UsrStat] ";
                 db.Database.ExecuteSqlCommand(sqlstr);

                sqlstr = "TRUNCATE TABLE [dbo].[CtrlActStat] ";
                 db.Database.ExecuteSqlCommand(sqlstr);
                 
                 return Json(new { success = true });
             }
             catch (Exception ex)
             {
             List<string> errors = new List<string> ();
             errors.Add(ex.Message + ". Error TRUNCATE TABLE [dbo].[UsrStat] or [CtrlActStat]");
                 return Json(new { success = false, errors= errors });
             }
         }


        
         public string TestAjax()
         {
             return "Ajax is ok";
         }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
