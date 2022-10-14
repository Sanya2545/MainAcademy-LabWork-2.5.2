using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using CRM_v2.Models;
using CRM_v2.DAL;

namespace CRM_v2.Account
{
    public partial class Login : Page
    {
        protected UsrStatStore usrststr = new UsrStatStore();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }


        //public void DeleteRole(string roleId)
        //{
        //    var roleUsers = _db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId));
        //    var role = _db.Roles.Find(roleId);

        //    foreach (var user in roleUsers)
        //    {
        //        this.RemoveFromRole(user.Id, role.Name);
        //    }
        //    _db.Roles.Remove(role);
        //    _db.SaveChanges();
        //}

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = new UserManager();
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);

                if (user != null)
                {

                    bool x = manager.IsLockedOut(user.Id);
                   
                    if (x)
                    {
                        usrststr.WriteStat(user.Id, "User is blocked");
                        FailureText.Text = "User is blocked";
                        ErrorMessage.Visible = true;
                    }

                    else
                    {
                        manager.ResetAccessFailedCount(user.Id);
                        //IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                        IdentityHelper.SignIn(manager, user, false);
                            // begin statistica
                        usrststr.WriteStat(user.Id, "Login");
                            // end statistica
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    }
                }
                else
                {
                    var r = manager.FindByName(UserName.Text);
                    if (r != null)
                    {
                        usrststr.WriteStat(r.Id, "Invalid password.");
                        FailureText.Text = "Invalid  password.";
                        ErrorMessage.Visible = true;
                        if (manager.IsLockedOut(r.Id))
                        {
                        }
                        else
                        {
                            if (manager.IsInRole(r.Id,"Administrator") == true)
                            {  }
                            else {
                                var z = manager.SetLockoutEnabled(r.Id, true);
                                manager.AccessFailed(r.Id); }
                            usrststr.WriteStat(r.Id, "Access denied.");

                        }
                    }
                    else
                    {
                        FailureText.Text = "Invalid  username.";
                        ErrorMessage.Visible = true;
                    }
                }
            }
        }
    }
}