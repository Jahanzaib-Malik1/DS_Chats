using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2
{
    public class Chat : Hub
    {
        Repo _repo;
        public Chat(Repo repo)
        {
            _repo = repo;
        }
        public Task Send(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
        public Task SendMessage(Messages message)
        {
            message.Chatid = 1;
            message.CreatedDate = DateTime.Now;
            return Clients.All.SendAsync("RecieveMessage",_repo.addMessage(message));
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
