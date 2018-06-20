using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Scores.Abstract;
using Scores.Hubs;

namespace Scores.Controllers
{
    public abstract class ApiHubController<T> : Controller
        where T : Hub
    {
        public IHubClients<IBroadcaster> Clients { get; }
        public IGroupManager Groups { get; private set; }
        protected ApiHubController(IHubContext<Broadcaster, IBroadcaster> broadcaster)
        {
            Clients = broadcaster.Clients;
        }
    }
}