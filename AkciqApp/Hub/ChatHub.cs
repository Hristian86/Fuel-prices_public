namespace AkciqApp.Hub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public ChatHub()
        {
        }

        public string Message { get; set; } = "sadasda";

        public async Task Send(string name, string message)
        {
            await Clients.All.SendAsync("Send", name, message);
        }

    }
}
