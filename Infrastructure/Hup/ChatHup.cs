using Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hup
{
    public  class ChatHup:Hub
    {
        
        private static Dictionary<int,HashSet<string>> Users = new();

        public override async Task OnConnectedAsync()
        {
            var userid = int.Parse(Context.UserIdentifier);
            var userName=Context.User?.Identity?.Name;//jwt 
            lock (Users)
            {
                if (!Users.ContainsKey(userid))
                    Users[userid]=new HashSet<string>();

                Users[userid].Add(Context.ConnectionId);

            }

            await Clients.All.SendAsync("User Online", 
                new { UserId = userid, UserName = userName });
            await base.OnConnectedAsync();
            
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userid = int.Parse(Context.UserIdentifier);
            var username = Context.User?.Identity?.Name;

             if (Users.ContainsKey(userid))
                Users[userid].Remove(Context.ConnectionId);

            await Clients.All.SendAsync("UserOffline", new { UserId = userid, UserName = username });
            await  base.OnDisconnectedAsync(exception);
        }



        public async Task JionChat(string ConversationId)
        => await Groups.AddToGroupAsync(Context.ConnectionId, ConversationId);

        public async Task LeaveChat(string ConversationId)
            => await Groups.RemoveFromGroupAsync(Context.ConnectionId, ConversationId);
        

    }
}
