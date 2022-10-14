using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CRM_v2.Models;
using CRM_v2.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;



namespace CRM_v2.Models
{

    public class CRMContext : DbContext
    {
        public DbSet<UsrStat> UsrStats { get; set; }
        public DbSet<CtrlActStat> CtrlActStats { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
    public class CtrlActStat
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string ActionParams {get; set;}
        public string OriginalUrl {get; set;}
        public string UserHost {get; set;}
        public string UserAgent { get; set; }
        public string  HttpMtthod {get; set;}
        public string Form { get; set; }
        public string Query { get; set; }
        public string Referer { get; set; }
        public string Hearders { get; set; }
        public string Cookies { get; set; }
        public string UserName { get; set; }
        public DateTime StartExecution { get; set; }
        public DateTime FinishExecution { get; set; }
        //public long ExecutionTime { get; set; }
    }

    public class UsrStat
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime LastLogin { get; set; }
        public bool isActive { get; set; }
        public string LastRole { get; set; }
        public bool isEnabled { get; set; }
        public string LastAction { get; set; }
        public DateTime LastActionTime { get; set; }
        public DateTime ModTime { get; set; }
        public UsrStat()
        { }
        public UsrStat(string _UserId, string _LastAction)
        {
            Id = 0;
                     Models.ApplicationDbContext context = new ApplicationDbContext();
                     var roleStore = new RoleStore<IdentityRole>(context);
                     var roleMgr = new RoleManager<IdentityRole>(roleStore);
                     var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                                                //var appUser = new ApplicationUser();
                     UserId = _UserId;
                     UserName = manager.FindById(_UserId).UserName;
                     LastLogin = DateTime.Now;
                     string n = "";
                     foreach (var r in manager.FindById(_UserId).Roles)
                     { n = n + " " + roleMgr.FindById(r.RoleId).Name; }
                     LastRole = n;
                     LastAction = _LastAction;
                     isEnabled = !(manager.IsLockedOut(_UserId));
                     isActive = true;
                     LastActionTime = DateTime.Now;
                     ModTime = DateTime.Now;


        }
    }
}