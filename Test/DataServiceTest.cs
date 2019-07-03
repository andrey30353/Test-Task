using System.Linq;
using CacheService.Customization;
using CacheService.Models;
using CacheService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Match = CacheService.Models.Match;

namespace Test
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void Check_GetSportMargin_Exist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MsSqlContext>()
                .UseInMemoryDatabase(databaseName: "Check_GetSportMargin_Exist")
                .Options;

            const int ConfigId = 1;
            const int SportId = 1;
            const double MarginValue = 1.2;

            using (var context = new MsSqlContext(options))
            {
                context.ConfigurationSportMargin.Add
                (
                    new ConfigurationSportMargin
                    {
                        ConfigurationId = ConfigId,
                        SportId = SportId,
                        MarginValue = MarginValue
                    }
                );
                context.Sports.Add(new Sport { SportId = SportId });
                context.SaveChanges();
            }

            using (var context = new MsSqlContext(options))
            {
                // Act
                var ds = new DataService(context);
                var sportMargin = ds.GetSportMargin(ConfigId, SportId);

                // Assert
                Assert.AreEqual(sportMargin, MarginValue);
            }
        }

        [TestMethod]
        public void Check_GetSportMargin_NotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MsSqlContext>()
                .UseInMemoryDatabase(databaseName: "Check_GetSportMargin_NotExist")
                .Options;

            using (var context = new MsSqlContext(options))
            {
                context.ConfigurationSportMargin.Add
                (
                    new ConfigurationSportMargin
                    {
                        ConfigurationId = 1,
                        SportId = 1,
                        MarginValue = 100
                    }
                );
                context.SaveChanges();
            }

            using (var context = new MsSqlContext(options))
            {
                // Act
                var ds = new DataService(context);
                var sportMargin = ds.GetSportMargin_Test(2, 2);

                // Assert
                Assert.AreEqual(sportMargin, 1);
            }
        }

        [TestMethod]
        public void Check_EnableConfigurations()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MsSqlContext>()
                .UseInMemoryDatabase(databaseName: "Check_EnableConfigurations")
                .Options;

            using (var context = new MsSqlContext(options))
            {
                context.Configuration.AddRange
                (
                    new Configuration { ConfigurationId = 1, IsEnabled = true },
                    new Configuration { ConfigurationId = 2, IsEnabled = true },
                    new Configuration { ConfigurationId = 3, IsEnabled = false }
                );
                context.SaveChanges();
            }

            using (var context = new MsSqlContext(options))
            {
                // Act
                var ds = new DataService(context);
                var enabledConfigs = ds.EnableConfigurations;

                // Assert
                Assert.AreEqual(2, enabledConfigs.Count);
                Assert.AreEqual(1, enabledConfigs[0].ConfigurationId);
                Assert.AreEqual(2, enabledConfigs[1].ConfigurationId);
            }
        }

        [TestMethod]
        public void Check_FilterDisabledMatches()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MsSqlContext>()
                .UseInMemoryDatabase(databaseName: "Check_FilterDisabledMatches")
                .Options;

            const int ConfigId = 1;
            using (var context = new MsSqlContext(options))
            {
                context.Matches.AddRange
                (
                    new Match { MatchId = 1, CategoryId = 1 },
                    new Match { MatchId = 2, CategoryId = 1 },
                    new Match { MatchId = 3, CategoryId = 1 },
                    new Match { MatchId = 4, CategoryId = 1 }
                );
                context.ConfigurationMatchDisabled.AddRange
                (
                    new ConfigurationMatchDisabled { ConfigurationId = ConfigId, MatchId = 2, Match = new Match { MatchId = 2, CategoryId = 1 } },
                    new ConfigurationMatchDisabled { ConfigurationId = ConfigId, MatchId = 4, Match = new Match { MatchId = 4, CategoryId = 1 } }
                );
                context.SaveChanges();
            }

            using (var context = new MsSqlContext(options))
            {
                // Act
                var ds = new DataService(context);
                var filterMatches = ds.FilterDisabledMatches_Test(ConfigId);

                // Assert
                Assert.AreEqual(2, filterMatches.Count());
                Assert.AreEqual(1, filterMatches[0].MatchId);
                Assert.AreEqual(3, filterMatches[1].MatchId);
            }
        }

        //[TestMethod]
        //public void Check_SportWithMatches()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<MsSqlContext>()
        //        .UseInMemoryDatabase(databaseName: "Check_SportWithMatches")
        //        .Options;

        //    using (var context = new MsSqlContext(options))
        //    {
        //        context.Sports.Add(new Sport { SportId = 1 });
        //        context.Categories.Add(new Category { CategoryId = 1, SportId = 1 });
        //        context.Matches.AddRange
        //        (
        //            new Match { MatchId = 1, CategoryId = 1 },
        //            new Match { MatchId = 2, CategoryId = 1 },
        //            new Match { MatchId = 3, CategoryId = 1 },
        //            new Match { MatchId = 4, CategoryId = 1, Name = "Match"}
        //        );
        //        context.SaveChanges();
        //    }

        //    using (var context = new MsSqlContext(options))
        //    {
        //        // Act
        //        var ds = new DataService(context);
        //        var sport = ds.SportWithMatches;

        //        // Assert
        //        Assert.AreEqual(1, sport.Count);
        //        Assert.AreEqual(1, sport[0].Categories.Count);
        //        Assert.AreEqual(4, sport[0].Categories.ToList()[0].Matches.Count);
        //        Assert.AreEqual("Match", sport[0].Categories.ToList()[0].Matches.ToList()[4].Name);
        //    }
        //}
    }
}
