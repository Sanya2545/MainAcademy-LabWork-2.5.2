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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace CRM_v2.Controllers
{
     [Authorize(Roles = "Administrator")]
    public class PrsnController : ContrlLoggerController
    {

        private IdentityAccessEntities db = new IdentityAccessEntities();

        private UsrPwd curUsrpwd = new UsrPwd()
        {
            Id = "",
            UserName = "",
            Pwd = "111111",
            Cnfpwd = "111111"
        };

        // GET: /Prsn/
        public async Task<ActionResult> Index()
        {
            return View(await db.AspNetUsers.ToListAsync());
        }


        // GET: /Prsn/Details/5      
        public async Task<ActionResult> Details([Bind(Include =  "Id,UserName,PasswordHash,SecurityStamp,Email,isClient,isStaff,AprClient,AprStaff,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] AspNetUser aspNetUser)
        {
            
            if ( aspNetUser.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = await db.AspNetUsers.FindAsync(aspNetUser.Id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        // GET: /Prsn/Create
       
        public ActionResult Create()
        {
            return View();
        }


        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include =  "Id,UserName,PasswordHash,SecurityStamp,Email,isClient,isStaff,AprClient,AprStaff,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                bool mod_isClient = (bool)aspNetUser.isClient;
                bool mod_isStaff = (bool)aspNetUser.isStaff;
                bool mod_AprClient = (bool)aspNetUser.AprClient;
                bool mod_AprStaff = (bool)aspNetUser.AprStaff;
                string mod_name = aspNetUser.UserName;

                int retu = CheckMod( mod_isClient, mod_isStaff, mod_AprClient, mod_AprStaff);

                if (retu == 0)
                {
                    db.Entry(aspNetUser).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    RoleActions roleActions = new RoleActions();
                    roleActions.modPrsn_role(mod_name, 0, mod_isStaff); roleActions.modPrsn_role(mod_name, 2, mod_isClient);
                    if (mod_isStaff || mod_isClient) roleActions.modPrsn_role(mod_name, 1, false);
                    return RedirectToAction("Index");
                }
                else if (retu == 2)
                { ViewBag.Comment = "Empty user is not allowed"; }
                else
                { ViewBag.Comment = "Roles error"; }
 
            }
            return View(aspNetUser);
        }

        internal int CheckMod( bool isClient, bool isStaff, bool AprClient, bool AprStaff)
        {
            int ret = 0;
            if (!((!isClient & !AprClient) & (!isStaff & !AprStaff)))
            {
                if ((
                   (((isClient & AprClient) & (isStaff & AprStaff)) | ((isClient & AprClient) & (!isStaff & !AprStaff))
                    )
                    | ((!isClient & !AprClient) & (isStaff & AprStaff))
                    ))                  
                {
                    
                }
                else
                    ret = 1;
            }
            else {
                ret = 2;
            }
            return ret;
        }


        // GET: /Prsn/Delete/5

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = await db.AspNetUsers.FindAsync(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);

        }

        internal async Task<int> WaitAsynchronouslyDel(AspNetUser usname)
        {
            int ret = 0;
            {
                RoleActions roleActions = new RoleActions();
                if (roleActions.AdminName!=usname.UserName)
                {
                    var r = await roleActions.delPrsn(usname);
                    string del_name = usname.UserName;
                    await db.SaveChangesAsync();  
                }
                
            }
            return ret;
        }


        // POST: /Prsn/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id,UserName,PasswordHash,SecurityStamp,Email,isClient,isStaff,AprClient,AprStaff,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] AspNetUser aspNetUser)
        {
            var u = await db.AspNetUsers.FindAsync(aspNetUser.Id);
            int retu = await WaitAsynchronouslyDel(u);
            return RedirectToAction("Index");
        }

        //// GET: /Prsn/MyAccess
        //public ActionResult MyAccess()
        //{
        //    return RedirectToAction("Index","AspNetUsers");
        //}


        internal async Task<bool> WaitAsynchronouslyRstPwd(AspNetUser usname, string Pwd)
        {
            RoleActions roleActions = new RoleActions();
            bool r = await roleActions.RstPwd_forPrsn(usname, Pwd);
            return r;
        }

        //beg rst
        // GET: /Prsn/ResetPassword/5

        public async Task<ActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = await db.AspNetUsers.FindAsync(id);
            if (u == null)
            {
                return HttpNotFound();
            }
// Reset pwd to username
            curUsrpwd.Id = id;
            curUsrpwd.UserName = u.UserName;
            curUsrpwd.Pwd = u.UserName;
            curUsrpwd.Cnfpwd = curUsrpwd.Pwd;

            return View(curUsrpwd);
        }
        //end rst

        // POST: AspNetUsers/ResetPassword/5       
        [HttpPost, ActionName("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPasswordConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u= await db.AspNetUsers.FindAsync(id);
            if (u == null)
            {
                return HttpNotFound();
            }

            curUsrpwd.Id = id;
            curUsrpwd.UserName = u.UserName;
            curUsrpwd.Pwd = u.UserName;
            curUsrpwd.Cnfpwd = curUsrpwd.Pwd;

            bool retu = await WaitAsynchronouslyRstPwd(u, curUsrpwd.Pwd);
            if (retu)
            { return RedirectToAction("Index"); }
            else
            {
                ViewBag.Message = "Password change error";
                return View(curUsrpwd);
            };

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: AspNetUsers/UsrLock/5

        public async Task<ActionResult> UsrLock(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            var manager = new UserManager();
            var r = await manager.FindByIdAsync(id);

            RoleActions roleActions = new RoleActions();
            if (roleActions.AdminName != r.UserName)
            {
                var z = manager.SetLockoutEnabled(r.Id, true);
                for (int i = 1; i <= manager.MaxFailedAccessAttemptsBeforeLockout; i++)
                    await manager.AccessFailedAsync(r.Id);               
            }

            return RedirectToAction("Index");
        }




        // GET: AspNetUsers/UsrUnlock/5

        public async Task<ActionResult> UsrUnlock(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            var manager = new UserManager();
            var r = await manager.FindByIdAsync(id);
            IdentityResult t = await manager.ResetAccessFailedCountAsync(r.Id);
            await manager.SetLockoutEndDateAsync(r.Id, DateTime.UtcNow);
            return RedirectToAction("Index");
        }
    }
}
