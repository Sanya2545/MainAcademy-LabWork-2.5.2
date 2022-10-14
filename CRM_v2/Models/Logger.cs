using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_v2.DAL;

namespace CRM_v2.Models
{
    public class Logger
    {
        private static readonly Logger instance = new Logger();
        private const Int32 _logsMaxCount = 10;
        private List<CtrlActStat> logs = new List<CtrlActStat>();
        public static Logger GetInstance()
        {
            return instance;
        }

        private Logger() { }

        public void AddLog(CtrlActStat log)
        {
            lock (logs)
            {
                logs.Add(log);
                if (logs.Count >= _logsMaxCount)
                {
                    SaveLogs();
                }
            }
        }

        public void Save()
        {
            lock (logs)
            {
                if (logs.Count > 0)
                {
                    SaveLogs();
                }
            }
        }
        private void SaveLogs()
        {

        using (var context = new CRMContext())
            {

                UnitOfWork unitOfWork = new UnitOfWork();
                unitOfWork.CtrlActStatRepository.InsertRange(logs);
                unitOfWork.Save();

            }
            logs = new List<CtrlActStat>();
        }

        public void SaveFin()
        {
            lock (logs)
            {
                if (logs.Count > 0)
                {
                    SaveLogsFin();
                }
            }
        }


        private void SaveLogsFin()
        {
            using (var context = new CRMContext())
            {
                context.CtrlActStats.AddRange(logs);
                context.SaveChanges();
            }         
        }
    }



    public class LoggerAttribute : ActionFilterAttribute
    {
        private CtrlActStat log;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            log = new CtrlActStat()
            {
                Action = (String)filterContext.RouteData.Values["Action"],
                Controller = (String)filterContext.RouteData.Values["Controller"],
                ActionParams = filterContext.ActionParameters == null ? String.Empty : filterContext.ActionParameters.ToString(),
                
                StartExecution = DateTime.Now,
                HttpMtthod = filterContext.HttpContext.Request.HttpMethod,
                Cookies = filterContext.HttpContext.Request.Cookies == null ? String.Empty : filterContext.HttpContext.Request.Cookies.ToString(),
                Hearders = filterContext.HttpContext.Request.Headers.ToString(),
                OriginalUrl = filterContext.HttpContext.Request.Url.ToString(),
                Referer = filterContext.HttpContext.Request.UrlReferrer == null ? String.Empty : filterContext.HttpContext.Request.UrlReferrer.ToString(),
                Form = filterContext.HttpContext.Request.Form == null ? String.Empty : filterContext.HttpContext.Request.Form.ToString(),
                Query = filterContext.HttpContext.Request.QueryString == null ? String.Empty : filterContext.HttpContext.Request.QueryString.ToString(),
                UserAgent = filterContext.HttpContext.Request.UserAgent,
                UserHost = filterContext.HttpContext.Request.UserHostAddress,
            };
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                log.UserName = filterContext.HttpContext.User.Identity.Name;
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            log.FinishExecution = DateTime.Now;
            Logger.GetInstance().AddLog(log);
        }
    }
}