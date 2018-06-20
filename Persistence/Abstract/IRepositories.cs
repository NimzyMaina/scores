using Scores.Models;

namespace Scores.Persistence.Abstract
{
    public interface IMatchRepository : IEntityBaseRepository<Match> { }
    public interface IFeedRepository : IEntityBaseRepository<Feed> { }
}