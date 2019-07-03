using System;
using CacheService.Customization;
using CacheService.Models;
using Microsoft.EntityFrameworkCore;

namespace CacheService.Services
{
    public class MsSqlContext : DbContext
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Match> Matches { get; set; }

        public DbSet<Market> Markets { get; set; }
        public DbSet<Outcome> Outcomes { get; set; }

        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<ConfigurationMatchDisabled> ConfigurationMatchDisabled { get; set; }
        public DbSet<ConfigurationSportMargin> ConfigurationSportMargin { get; set; }

        public DbSet<TableVersion> TableVersion { get; set; }

        public MsSqlContext()
        {
            Database.EnsureCreated();
        }

        public MsSqlContext(DbContextOptions<MsSqlContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=sport;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>().HasData
            (
                new Sport { SportId = 1, Name = "Soccer" },
                new Sport { SportId = 2, Name = "Volleyball" }
            );
            modelBuilder.Entity<Category>().HasData
            (
                new Category { CategoryId = 1, SportId = 1, Name = "World cup" },
                new Category { CategoryId = 2, SportId = 1, Name = "City cup" },

                new Category { CategoryId = 3, SportId = 2, Name = "Volleyball local cup" },
                new Category { CategoryId = 4, SportId = 2, Name = "Volleyball tournament" }
            );
            modelBuilder.Entity<Match>().HasData
            (
                new Match { MatchId = 1, CategoryId = 1, Name = "England vs Cameroon", MatchDate = DateTime.Now.AddHours(1) },
                new Match { MatchId = 2, CategoryId = 1, Name = "Brazil vs Australia", MatchDate = DateTime.Now.AddHours(2) },

                new Match { MatchId = 3, CategoryId = 2, Name = "Cameroon vs England", MatchDate = DateTime.Now.AddHours(3) },
                new Match { MatchId = 4, CategoryId = 2, Name = "Bra vs Aus", MatchDate = DateTime.Now.AddHours(4) },

                new Match { MatchId = 5, CategoryId = 3, Name = "Germany vs Sweden", MatchDate = DateTime.Now.AddHours(5) },
                new Match { MatchId = 6, CategoryId = 3, Name = "Money vs Time", MatchDate = DateTime.Now.AddHours(6) },
                new Match { MatchId = 7, CategoryId = 3, Name = "Team 1 vs Team 2", MatchDate = DateTime.Now.AddHours(25) }
            );

            modelBuilder.Entity<Market>().HasData
            (
                new Market { MarketId = 1, MatchId = 1, Name = "1x2" },
                new Market { MarketId = 2, MatchId = 1, Name = "Exact Goals" },
                new Market { MarketId = 3, MatchId = 1, Name = "Odd/Even" },

                new Market { MarketId = 4, MatchId = 2, Name = "Odd/Even" },

                new Market { MarketId = 5, MatchId = 6, Name = "1x2" }
            );
            modelBuilder.Entity<Outcome>().HasData
            (
                new Outcome { OutcomeId = 1, MarketId = 1, Name = "1", Price = 1.14 },
                new Outcome { OutcomeId = 2, MarketId = 1, Name = "2", Price = 6.8 },
                new Outcome { OutcomeId = 3, MarketId = 1, Name = "x", Price = 16.0 },

                new Outcome { OutcomeId = 4, MarketId = 4, Name = "Odd", Price = 1.3 },
                new Outcome { OutcomeId = 5, MarketId = 4, Name = "Even", Price = 5.5 },

                new Outcome { OutcomeId = 6, MarketId = 5, Name = "1", Price = 2 },
                new Outcome { OutcomeId = 7, MarketId = 5, Name = "2", Price = 3 }
            );

            modelBuilder.Entity<Configuration>().HasData
            (
                new Configuration { ConfigurationId = 1, Name = "Client enable", IsEnabled = true },
                new Configuration { ConfigurationId = 2, Name = "Client enable 2", IsEnabled = true },
                new Configuration { ConfigurationId = 3, Name = "Client disable", IsEnabled = false }
            );
            modelBuilder.Entity<ConfigurationMatchDisabled>()
                .HasKey(t => new { t.ConfigurationId, t.MatchId });
            modelBuilder.Entity<ConfigurationMatchDisabled>().HasData
            (
                new ConfigurationMatchDisabled { ConfigurationId = 1, MatchId = 1 },
                new ConfigurationMatchDisabled { ConfigurationId = 2, MatchId = 7 }
            );
            modelBuilder.Entity<ConfigurationSportMargin>()
                .HasKey(t => new { t.ConfigurationId, t.SportId });
            modelBuilder.Entity<ConfigurationSportMargin>().HasData
            (
                new ConfigurationSportMargin { ConfigurationId = 1, SportId = 1, MarginValue = 1.2 },
                new ConfigurationSportMargin { ConfigurationId = 2, SportId = 2, MarginValue = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
