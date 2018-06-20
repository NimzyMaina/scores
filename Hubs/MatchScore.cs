using System.ComponentModel.DataAnnotations;

namespace Scores.Hubs
{
    public class MatchScore
    {
        [Required]
        public int HostScore { get; set; }
        [Required]
        public int GuestScore {get; set;}
    }
}