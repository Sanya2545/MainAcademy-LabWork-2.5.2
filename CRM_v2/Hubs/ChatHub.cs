using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using CRM_v2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CRM_v2.Hubs
{

    public class SignalRUser
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }

        public SignalRUser(string tmpConnId, string tmpName)
        {

                var manager = new UserManager();             
                {
                    ConnectionId = tmpConnId;
                    Id = manager.FindByName(tmpName).Id;               
                    Name = tmpName;
                };
        }

    }
    //[Authorize(Roles = "Administrator,Staff")] 
    public class ChatHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}
        protected UsrStatStore usrststr = new UsrStatStore();
        static List<SignalRUser> Users = new List<SignalRUser>();
        public override Task OnConnected()
        {
            
            //We finally broadcast the complete list of online users to all connected clients
            var id = Context.ConnectionId;
            var r = GetAuthInfo();
            SignalRUser us = new SignalRUser(id, r);
            usrststr.WriteStat(us.Id, " new participant");
            return Clients.All.joined(GetAuthInfo());
           
        }

     public string GetAuthInfo()
        {
            var user = Context.User;
            string tmpstr = user.Identity.Name;
            return tmpstr;
        }

        // New participant connect
        public void Connect()
        {
            var id = Context.ConnectionId;


            if (Users.Count(x => x.ConnectionId == id) == 0)
            {
                var r = GetAuthInfo();

                Users.Add(new SignalRUser (id,r));

                // Send message to current user
                Clients.Caller.onConnected(id, r, Users);

                // Send message to all users but not for current user
                Clients.AllExcept(id).onNewUserConnected(id, r);
            }
        }



        // Disconnect user
        public override System.Threading.Tasks.Task OnDisconnected()
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }
            usrststr.WriteStat(item.Id, "User disconnected ");
            return base.OnDisconnected();
        }
        public void Send( string message)
        {
            // Call the addNewMessageToPage method to update clients.
            string tmpstr = DateTime.Now.ToLocalTime()+"  " +message;
            Clients.All.addNewMessageToPage(Context.User.Identity.Name, tmpstr);
        }
    }
}