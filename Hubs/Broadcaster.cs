using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Scores.Abstract;

namespace Scores.Hubs
{
    public class Broadcaster : Hub<IBroadcaster>
    {
        public override Task OnConnectedAsync()
        {
            // Set connection id for just connected client only
            return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
        }

        // Server side methods called from client
        public Task Subscribe(int matchId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, matchId.ToString());
        }

        public Task Unsubscribe(int matchId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, matchId.ToString());
        }
    }
}