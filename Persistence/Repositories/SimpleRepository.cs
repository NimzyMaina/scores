using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scores.Models;
using Scores.Persistence.Abstract;

namespace Scores.Persistence.Repositories
{
    public class SimpleRepository : ISimpleRepository
    {
        private readonly AppDbContext context;

        public SimpleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Match> GetMatchAsync(int id)
        {
            return await context.Matches.FindAsync(id);
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync()
        {
           return await context.Matches
                .Include(m => m.Feeds)
                .ToListAsync();
        }

        public void UpdateAsync(Match match)
        {
            context.Matches.Update(match);
        }
    }
}