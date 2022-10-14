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
using PagedList;

namespace CRM_v2.Controllers
{

    public class StatisticaController : ContrlLoggerController
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Find(string term)
        {

            var statistica = unitOfWork.UsrStatRepository.Get(usr => usr.UserName.StartsWith(term));
            var projection =  from usrst in statistica
                             select new
                             {
                                 label = usrst.UserName,
                                 value = usrst.UserName,
                             };

            var distinctWords = projection.Distinct();
            return Json(distinctWords.ToList(),
              JsonRequestBehavior.AllowGet);

        }


      // GET: /Statistica/

        public ViewResult Index( string currentFilter, string searchString, int? page)
      {
         int pageSize = 10;
         int pageNumber = (page ?? 1);
         IEnumerable<UsrStat> statistica;
               if (searchString != null)
                   {
                      page = 1;
                   }
                   else
                   {
                      searchString = currentFilter;
                   }

                   ViewBag.CurrentFilter = searchString;
                           if (!String.IsNullOrEmpty(searchString))
                           {
                                statistica = unitOfWork.UsrStatRepository.Get(usr => usr.UserName.StartsWith(searchString), orderBy: usr => usr.OrderByDescending(d => d.ModTime));
                           }
                           else
                           {
                                statistica = unitOfWork.UsrStatRepository.Get( orderBy: usr => usr.OrderByDescending(d => d.ModTime));
                           }
             return View(statistica.ToPagedList(pageNumber, pageSize));   
      }



      // GET: /Statistica/Details/5
        
      public ViewResult Details(int id)
      {
         UsrStat statistica = unitOfWork.UsrStatRepository.GetByID(id);
         return View(statistica);
      }


       // GET: /Statistica/Edit/5
        
      public ActionResult Edit(int id)
      {
         UsrStat statistica = unitOfWork.UsrStatRepository.GetByID(id);
          return View(statistica);
      }
       // POST: /Statistica/Edit/5
        
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(
           [Bind(Include = "Id,UserName,LastLogin,LastAction")]
         UsrStat statistica)
      {
         try
         {
            if (ModelState.IsValid)
            {
               unitOfWork.UsrStatRepository.Update(statistica);
               unitOfWork.Save();
               return RedirectToAction("Index");
            }
         }
         catch (DataException /* dex */)
         {
            //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
         }
                        
         return View(statistica);
      }

      // GET: /Statistica/Delete/5
    
      public ActionResult Delete(int id)
      {
         UsrStat statistica = unitOfWork.UsrStatRepository.GetByID(id);
         return View(statistica);
      }

      //
      // POST: /Statistica/Delete/5
        
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         UsrStat course = unitOfWork.UsrStatRepository.GetByID(id);
         unitOfWork.UsrStatRepository.Delete(id);
         unitOfWork.Save();
         return RedirectToAction("Index");
      }

      protected override void Dispose(bool disposing)
      {
         unitOfWork.Dispose();
         base.Dispose(disposing);
      }


      // GET: /Statistica/Create
        
      public ActionResult Create()

      {
          return View();
      }

        // POST: /Statistica/Create
         
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(
          [Bind(Include = "Id,UserName,LastLogin,LastRole,LastAction, ModTime,isActive, isEnabled, LastActionTime")]
         UsrStat statistica)
      {
          try
          {
              if (ModelState.IsValid)
              {
                  string tmpId = User.Identity.GetUserId();
                  UsrStat write_st = new UsrStat(tmpId, "statistica.LastAction");
                  unitOfWork.UsrStatRepository.Insert(write_st);
                  unitOfWork.Save();
                  return RedirectToAction("Index");
              }
          }
          catch (DataException /* dex */)
          {
              //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
              ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
          }
     
          return View(statistica);
      }


         //// POST: /Statistica/CreateByUsr
         //[HttpPost]
         
         public void CreateByUsr()
         {
             try
             {

                 if (ModelState.IsValid)
                 {
                     string tmpId = User.Identity.GetUserId();
                     UsrStat write_st = new UsrStat(tmpId,"Login");
                     unitOfWork.UsrStatRepository.Insert(write_st);
                     unitOfWork.Save();
                     return ;
                 }
             }
             catch (DataException /* dex */)
             {
                 //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                 ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
             }

             return ;
         }
   }
}