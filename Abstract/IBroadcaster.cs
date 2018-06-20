using System.Threading.Tasks;
using Scores.Dtos;

namespace Scores.Abstract
{
    // Client side methods to be invoked by Broadcaster Hub
    public interface IBroadcaster
    {
        Task SetConnectionId(string connectionId);
        Task UpdateMatch(MatchDto match);
        // Task AddFeed(FeedViewModel feed);
        // Task AddChatMessage(ChatMessage message);
    }
}