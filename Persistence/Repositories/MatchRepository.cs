using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scores.Models;
using Scores.Persistence.Abstract;

namespace Scores.Persistence.Repositories
{
    public class MatchRepository : EntityBaseRepository<Match>, IMatchRepository
    {
        public MatchRepository(AppDbContext context)
            : base(context)
        { }
    }
}