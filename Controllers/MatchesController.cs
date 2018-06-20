using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Scores.Abstract;
using Scores.Dtos;
using Scores.Hubs;
using Scores.Models;
using Scores.Persistence.Abstract;

namespace Scores.Controllers
{
    [Route("api/[controller]")]
    public class MatchesController : ApiHubController<Broadcaster>
    {
        private readonly ISimpleRepository simp;
        private readonly IUnitOfWork uof;
        private readonly IMapper mapper;
        //private readonly IMatchRepository repo;

        public MatchesController(
            IHubContext<Broadcaster,IBroadcaster> Broadcaster,
            ISimpleRepository simp,
            IUnitOfWork uof,
            IMapper mapper
            //IMatchRepository repo
            ): base(Broadcaster)
        {
            this.simp = simp;
            this.uof = uof;
            this.mapper = mapper;
            //this.repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<MatchDto>> GetMatches()
        {
            //var matches = await repo.AllIncludingAsync(m => m.Feeds);
            var matches = await simp.GetMatchesAsync();

            return mapper.Map<IEnumerable<Match>, IEnumerable<MatchDto>>(matches);
        }

        // PUT api/matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MatchScore score)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var match =  await simp.GetMatchAsync(id);

            if(match == null)
            {
                return NotFound();
            }

            if (score.HostScore == 0 && score.GuestScore == 0)
            {
                match.Feeds.Clear();
            }

            match.HostScore = score.HostScore;
            match.GuestScore = score.GuestScore;

            simp.UpdateAsync(match);

            await uof.Complete();

            var m = mapper.Map<Match,MatchDto>(match);

            await Clients.All.UpdateMatch(m);

            return Ok(m);

        }
    }
}