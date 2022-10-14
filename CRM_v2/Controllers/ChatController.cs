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

    public class ChatController : ContrlLoggerController
    {
       
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }


    }
}