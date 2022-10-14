using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using CRM_v2.Models;

namespace CRM_v2.Controllers
{
      [Authorize(Roles = "Administrator,Client,Staff")]
    public class PictureController : ContrlLoggerController
    {
        // GET: /Picture
        public ActionResult Index()
        {
            return View(new PictureModel());
        }

        // GET: /Picture/Upload
        public ActionResult Upload()
        {
            return View();
        }

          //POST: /Picture/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(FormCollection form)
        {
            HttpPostedFileBase hpf = Request.Files["hpf"] as HttpPostedFileBase;

                string fileName = System.IO.Path.GetFileName(hpf.FileName);

                Picture image = new Picture(fileName, Request["description"]);
                PictureModel PictModel = new PictureModel();
                PictModel.Add(image, hpf);
            
            return RedirectToAction("Index");
        }

    }
}