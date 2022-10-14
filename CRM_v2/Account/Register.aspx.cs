using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using CRM_v2.Models;

namespace CRM_v2.Account
{
    public partial class Register : Page
    {
        protected UsrStatStore usrststr = new UsrStatStore();
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = new UserManager();

            var user = new ApplicationUser() { UserName = UserName.Text,isClient=isClient.Checked,isStaff=isStaff.Checked,Email=Email.Text,AprClient=false,AprStaff=false };
            IdentityResult result = manager.Create(user, Password.Text);
            IdentityResult IdUserResult;
            if (result.Succeeded)
            {


                // If the new "User" user was successfully created, 
                // add the "User" user to the "User" role. 

                    IdUserResult = manager.AddToRole(user.Id, "User");
                    if (!IdUserResult.Succeeded)
                    {
                        // Handle the error condition if there's a problem adding the user to the role.
                        usrststr.WriteStat(user.Id, "User role was not added.");
                    }


                IdentityHelper.SignIn(manager, user, isPersistent: false);
                usrststr.WriteStat(user.Id, "Login");
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}