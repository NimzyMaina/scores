using System.Collections.Generic;
using System.Threading.Tasks;
using Scores.Models;

namespace Scores.Persistence.Abstract
{
    public interface ISimpleRepository
    {
        Task<IEnumerable<Match>> GetMatchesAsync();

        void UpdateAsync(Match match);

        Task<Match> GetMatchAsync(int id);

    }
}