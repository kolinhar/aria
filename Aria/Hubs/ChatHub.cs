using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Aria.Hubs
{
    public class ChatHub : Hub
    {

        private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
            new Aria.DB.MongoDb().RegisterNewMessage(name, DateTime.Now, message);
        }

        private void CallUserName(string connectionId)
        {
            Clients.Caller.callBackUserName();
        }

        public void RegisterUserName(string name)
        {
            Users.Add(Context.ConnectionId, name);
            Clients.All.broadCastUsersList(Users);
        }

        public override Task OnConnected()
        {
            CallUserName(Context.ConnectionId);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            if (Users.ContainsKey(Context.ConnectionId))
            {
                Users.Remove(Context.ConnectionId);
                Clients.All.broadCastUsersList(Users);
            }
            return base.OnDisconnected(stopCalled);
        }

    }
}