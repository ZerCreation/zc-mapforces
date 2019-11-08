using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ZerCreation.MapForces.WebApi.HubConfig
{
    public class GameHub : Hub
    {
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
