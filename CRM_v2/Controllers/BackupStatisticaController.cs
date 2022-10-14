using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_v2.Models;

namespace CRM_v2.Controllers
{
    public class BackupStatisticaController : ContrlLoggerController
    {
        private CRMContext context = new CRMContext();

        // GET: BackupStatistica
        public ActionResult Index()
        {
          
            return PartialView("_Index"); ;
        }

        // ! ValidateAntiForgeryToken does not work with Ajax.ActionLink - so jQuiery modalform.js!

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CopyTbl()
        {
        string strsql = "select * into [dbo].[UsrStat" + DateTime.Now.ToString("ddMMyyyyhhmm") + "] from [dbo].[UsrStat]";
        string strsql1 = "select * into [dbo].[CtrlActStat" + DateTime.Now.ToString("ddMMyyyyhhmm") + "] from [dbo].[CtrlActStat]";
        string sqlstr2 = "delete from [dbo].[UsrStat]";
        string sqlstr3 = "delete from [dbo].[CtrlActStat]";
     
//
        using (var dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
        {
            try
            {

                context.Database.ExecuteSqlCommand(strsql);
                context.Database.ExecuteSqlCommand(sqlstr2);   
                context.Database.ExecuteSqlCommand(strsql1);                                           
                context.Database.ExecuteSqlCommand(sqlstr3);

                context.SaveChanges();

                dbContextTransaction.Commit();
                return Json(new { success = true });
            }
            catch (Exception)
            {
                dbContextTransaction.Rollback();
                List<string> errors = new List<string>();
                errors.Add("Error " + strsql + " or " + strsql1);
                return Json(new { success = false, errors = errors });
            }

        }
        }
    }
}