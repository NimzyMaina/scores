using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scores.Models
{
    public class Match : IEntityBase
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Guest { get; set; }
        public int HostScore { get; set; }
        public int GuestScore { get; set; }
        public DateTime MatchDate { get; set; }
        public MatchTypeEnums Type { get; set; }

        public ICollection<Feed> Feeds { get; set; }

        public Match()
        {
            Feeds = new Collection<Feed>();
        }
    }
}