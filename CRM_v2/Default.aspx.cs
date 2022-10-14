using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM_v2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var r = Page.User.IsInRole("Administrator");
        }


        protected void LogIn(object sender, EventArgs e)
        {
            var r = Page.User.IsInRole("Administrator");
           
            Response.Redirect("~/Adminka");
        }

        protected void GalIn(object sender, EventArgs e)
        {
            var r = Page.User.IsInRole("Administrator,Client,User,Staff");

            Response.Redirect("~/Picture");
        }

        public void Page_LoadComplete(object sender, EventArgs e)
        {
            admpanel.Visible = true;
            admctrl.Visible = true;
            chatpanel.Visible = true;

            if (!(Page.User.IsInRole("Administrator")))
            {
                admpanel.Visible = false;
                admctrl.Visible = false;
                chatpanel.Visible = false;
                if (Page.User.IsInRole("Staff"))
                {
                    chatpanel.Visible = true;
                }
            }

        }
    }
}