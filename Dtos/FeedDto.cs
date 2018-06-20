using System;
using Scores.Models;

namespace Scores.Dtos
{
    public class FeedDto: IEntityBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MatchId { get; set; }
    }
}