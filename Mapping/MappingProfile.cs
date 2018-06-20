using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scores.Dtos;
using Scores.Models;

namespace Scores.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<Match, MatchDto>()
                .ForMember(vm => vm.Type, map => map.MapFrom(m => m.Type.ToString()))
                .ForMember(vm => vm.Feeds, map => map.MapFrom(m => m.Feeds.Select(mf => 
                    new FeedDto{Id = mf.Id, Description = mf.Description, CreatedAt = mf.CreatedAt, MatchId = mf.MatchId}
                )));
            CreateMap<Feed, FeedDto>();
        }
    }
}