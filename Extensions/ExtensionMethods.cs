using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scores.Models;
using Scores.Persistence;
using Scores.Persistence.Abstract;
using Scores.Persistence.Repositories;

namespace Scores.Extensions
{
    public static class ExtensionMethods
    {
        public static IServiceCollection ConfigureDI(this IServiceCollection services,IConfiguration configuration)
        {
            // Add Automapper
            services.AddAutoMapper();
            // Add SQL Server config
            // services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            //services.AddScoped<IMatchRepository,MatchRepository>();
            services.AddScoped<ISimpleRepository, SimpleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IWebHost Migrate(this IWebHost webHost)
        {
             var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDbContext>();

                if (!dbContext.Matches.Any())
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
                    dbContext.Matches.Add(match_01); dbContext.Matches.Add(match_02);
                    dbContext.SaveChanges();
                }


            }

            return webHost;
        }
    }
}