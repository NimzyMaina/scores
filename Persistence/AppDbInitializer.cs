using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Scores.Models;

namespace Scores.Persistence
{
    public class AppDbInitializer
    {
        private static AppDbContext context;
        public static void Initalize(IServiceProvider serviceProvider)
        {
            context = (AppDbContext)serviceProvider.GetService(typeof(AppDbContext));

            InitializeSchedules();
        }

        private static void InitializeSchedules()
        {
            if(!context.Matches.Any())
            {
                Match match_01 = new Match
                {
                    Host = "Team 1",
                    Guest = "Team 2",
                    HostScore = 0,
                    GuestScore = 0,
                    MatchDate = DateTime.Now,
                    Type = MatchTypeEnums.Basketball,
                    Feeds = new Collection<Feed>
                    {
                        new Feed()
                        {
                            Description = "Match started",
                            MatchId = 1,
                            CreatedAt = DateTime.Now
                        }
                    }
                };

                Match match_02 = new Match
                {
                    Host = "Team 3",
                    Guest = "Team 4",
                    HostScore = 0,
                    GuestScore = 0,
                    MatchDate = DateTime.Now,
                    Type = MatchTypeEnums.Basketball,
                    Feeds = new Collection<Feed>
                    {
                        new Feed()
                        {
                            Description = "Match started",
                            MatchId = 2,
                            CreatedAt = DateTime.Now
                        }
                    }
                };

                context.Matches.Add(match_01); context.Matches.Add(match_02);

                context.SaveChanges();
            }
        }
    }
}