using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CRM_v2.Models;
using System.Reflection;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using System.Data;
using System.Web.Security;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;



namespace CRM_v2.Models
{
    internal class RoleActions
    {

        //internal RoleActions()
        //{
        //    AdminName = "Staffadmin";
        //}
        //public string AdminName { get; set; }
        internal RoleActions()
        {
        }

        private string adminname = "Staffadmin";

        public string AdminName
        {
            get
            {
                return adminname;
            }
            protected set
            {
                adminname = value;
            }
        }
    
        //MY_MY_add
        internal void createUserClient()
        {
            //throw new NotImplementedException();
            // Access the application context and create result variables.
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            //IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            string[] rolearr =new string[] {"User","Client"};
            // Then, you create the "User" role if it doesn't already exist.

            foreach (string rolename in rolearr)
            {
                if (!roleMgr.RoleExists(rolename))
                {
                    IdRoleResult = roleMgr.Create(new IdentityRole(rolename));
                    if (!IdRoleResult.Succeeded)
                    {
                        // Handle the error condition if there's a problem creating the RoleManager object.
                    }
                }
            }




        }
        internal bool createAdmin()
        {
            //throw new NotImplementedException();
            // Access the application context and create result variables.
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            bool goodadmin = true;
            bool goodadmin1 = true;
            bool admcr = false;
            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);
            //Check admin existence
           

            // Then, you create the "Administrator" role if it doesn't already exist.
            if (!roleMgr.RoleExists("Administrator"))
            {
                
                IdRoleResult = roleMgr.Create(new IdentityRole("Administrator"));
                
                if (!IdRoleResult.Succeeded)
                {
                    // Handle the error condition if there's a problem creating the RoleManager object.
                    goodadmin = false;
                }
            }
            if (goodadmin)
            {
                var admrole = roleMgr.FindByName("Administrator");
                if (admrole.Users.Count == 0)
                {
                    goodadmin1 = false;
                }                   
            }


            if ((goodadmin)&&(!goodadmin1))
            {
                // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
                // object. Note that you can create new objects and use them as parameters in
                // a single line of code, rather than using multiple lines of code, as you did
                // for the RoleManager object.
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var appUser = new ApplicationUser()
                {
                    UserName = AdminName,
                    //MY_MY_ADD
                    isClient = false,
                    isStaff = true,
                    AprClient = false,
                    AprStaff = true
                };
                IdUserResult = userMgr.Create(appUser, "Pa$$word");

                // If the new "Admin" user was successfully created, 
                // add the "Admin" user to the "Administrator" role. 
                if (IdUserResult.Succeeded)
                {
                    admcr = true;
                    IdUserResult = userMgr.AddToRole(appUser.Id, "Administrator");
                    if (!IdUserResult.Succeeded)
                    {
                        // Handle the error condition if there's a problem adding the user to the role.
                        admcr = false;
                    }
                }
                else
                {
                    // Handle the error condition if there's a problem creating the new user. 
                }
            }
            
            return admcr;
        }

        //MY_MY_ADD

        internal void createStaffRole()
        {
            //throw new NotImplementedException();
            // Access the application context and create result variables.
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "Administrator" role if it doesn't already exist.
            if (!roleMgr.RoleExists("Staff"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole("Staff"));
                if (!IdRoleResult.Succeeded)
                {
                    // Handle the error condition if there's a problem creating the RoleManager object.
                }
            }

        }

       // del begin
        internal async Task<IdentityResult> delPrsn(AspNetUser Name_for_ctrl)
        {
           
                Models.ApplicationDbContext cdb = new ApplicationDbContext(); 
                {
                            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(cdb));
                            var user = userManager.Users.SingleOrDefault(u => u.UserName == Name_for_ctrl.UserName);
                   IdentityResult r = await userManager.DeleteAsync(user);
            
                    cdb.SaveChanges();
                    return r;
                }
        }
        //del end


        internal async Task<bool> RstPwd_forPrsn(AspNetUser Name_for_ctrl, string Pwd)
        {
            bool flag = false;
            var id = Name_for_ctrl.Id;
            Models.ApplicationDbContext db = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.Users.SingleOrDefault(u => u.Id == id);
            //string code = await userManager.GeneratePasswordResetTokenAsync(id);
            try
            {
                //beg
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(db);
                String newPassword = Pwd; //"<PasswordAsTypedByUser>";
                String hashedNewPassword = userManager.PasswordHasher.HashPassword(newPassword);
                await store.SetPasswordHashAsync(user, hashedNewPassword);
                await store.UpdateAsync(user);
                //end

                flag = true;
            }
            catch { flag = false; }

            return flag;
        }


        internal void modPrsn_role(string Name_for_ctrl, int rl_nmb, bool flag)
        {
            try
            {
                string UserName = Name_for_ctrl;
                Models.ApplicationDbContext cdb = new ApplicationDbContext();
                var adm_customer = cdb.Users.Where(c => c.UserName == UserName).FirstOrDefault();
                    cdb.SaveChanges();
                        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(cdb));
                        //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(cdb));
                        //var tmpus = new ApplicationUser { UserName = adm_customer.UserName };
                        string[] arr2Lines;
                        arr2Lines = new string[3] { "Staff", "User","Client" };
                        if (flag)
                        { var result = userManager.AddToRole(adm_customer.Id, arr2Lines[rl_nmb]); }
                        else
                        { var result = userManager.RemoveFromRole(adm_customer.Id, arr2Lines[rl_nmb]); }
                       
            }
            finally
            { }
        }

       

    }
}