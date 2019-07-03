using System;
using System.Collections.Generic;
using System.Linq;
using CacheService.Customization;
using CacheService.Models;
using CacheService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Match = CacheService.Models.Match;

namespace Test
{
    [TestClass]
    public class DataMapServiceTest
    {
        [TestMethod]
        public void Check_CreateSportMenu_MatchCount()
        {
            // Arrange
            var mock = CreateDataServiceMock();
            var dataMapMock = new DataMapService(mock.Object);

            // act
            var result = dataMapMock.CreateSportMenu();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0].SportList.Count);
            Assert.AreEqual(4, result[0].SportList[0].MatchCount);
            Assert.AreEqual(3, result[0].SportList[1].MatchCount);
        }

        [TestMethod]
        public void Check_EventDetails_ApplyMargin()
        {
            // Arrange
            const double MarginValue = 10;
            var mock = CreateDataServiceMock();
            mock.Setup(t => t.GetSportMargin(1, 1)).Returns(MarginValue);
            mock.Setup(t => t.GetSportMargin(It.IsNotIn(1), It.IsAny<int>())).Returns(1);
            mock.Setup(t => t.GetSportMargin(It.IsAny<int>(), It.IsNotIn(1))).Returns(1);
            var dataMapMock = new DataMapService(mock.Object);

            // act
            var configList = dataMapMock.CreateEventDetails();

            // Assert
            Assert.AreEqual(1.14 * MarginValue, configList[0].EventDetailsList[0].Markets[0].Outcomes[0].Price);
            Assert.AreEqual(1.14, configList[1].EventDetailsList[0].Markets[0].Outcomes[0].Price);
        }

        private Mock<IDataService> CreateDataServiceMock()
        {
            var mock = new Mock<IDataService>();

            var conf1 = new Configuration { ConfigurationId = 1, Name = "Client enable", IsEnabled = true };
            var conf2 = new Configuration { ConfigurationId = 2, Name = "Client enable 2", IsEnabled = false };

            var s1 = new Sport { SportId = 1, Name = "Soccer" };
            var s2 = new Sport { SportId = 2, Name = "Volleyball" };

            var c1 = new Category { CategoryId = 1, SportId = 1, Sport = s1, Name = "World cup" };
            var c2 = new Category { CategoryId = 2, SportId = 1, Sport = s1, Name = "City cup" };
            var c3 = new Category { CategoryId = 3, SportId = 2, Sport = s2, Name = "Volleyball local cup" };
            var c4 = new Category { CategoryId = 4, SportId = 2, Sport = s2, Name = "Volleyball tournament" };

            var m1 = new Match { MatchId = 1, CategoryId = 1, Category = c1, Name = "England vs Cameroon", MatchDate = DateTime.Now.AddHours(1) };
            var m2 = new Match { MatchId = 2, CategoryId = 1, Category = c1, Name = "Brazil vs Australia", MatchDate = DateTime.Now.AddHours(2) };
            var m3 = new Match { MatchId = 3, CategoryId = 2, Category = c2, Name = "Cameroon vs England", MatchDate = DateTime.Now.AddHours(3) };
            var m4 = new Match { MatchId = 4, CategoryId = 2, Category = c2, Name = "Bra vs Aus", MatchDate = DateTime.Now.AddHours(4) };
            var m5 = new Match { MatchId = 5, CategoryId = 3, Category = c3, Name = "Germany vs Sweden", MatchDate = DateTime.Now.AddHours(5) };
            var m6 = new Match { MatchId = 6, CategoryId = 3, Category = c3, Name = "Money vs Time", MatchDate = DateTime.Now.AddHours(6) };
            var m7 = new Match { MatchId = 7, CategoryId = 3, Category = c3, Name = "Team 1 vs Team 2", MatchDate = DateTime.Now.AddHours(25) };
            var allMatches = new List<Match> { m1, m2, m3, m4, m5, m6, m7 };

            var mark1 = new Market { Match = m1, Name = "1x2" };
            var mark2 = new Market { Match = m1, Name = "Exact Goals" };
            var mark3 = new Market { Match = m1, Name = "Odd/Even" };

            var o1 = new Outcome { Market = mark1, Name = "1", Price = 1.14 };
            var o2 = new Outcome { Market = mark1, Name = "2", Price = 6.8 };
            var o3 = new Outcome { Market = mark1, Name = "x", Price = 16.0 };

            mark1.Outcomes = new List<Outcome> { o1, o2, o3 };
            mark2.Outcomes = new List<Outcome>();
            mark3.Outcomes = new List<Outcome>();

            m1.Markets = new List<Market> { mark1, mark2, mark3 };
            m2.Markets = new List<Market>();
            m3.Markets = new List<Market>();
            m4.Markets = new List<Market>();
            m5.Markets = new List<Market>();
            m6.Markets = new List<Market>();
            m7.Markets = new List<Market>();

            c1.Matches = new List<Match> { m1, m2 };
            c2.Matches = new List<Match> { m3, m4 };
            c3.Matches = new List<Match> { m5, m6, m7 };
            c4.Matches = new List<Match>();

            s1.Categories = new List<Category> { c1, c2 };
            s2.Categories = new List<Category> { c3, c4 };

            mock.SetupGet(t => t.EnableConfigurations).Returns(
                new List<Configuration> { conf1, conf2 });

            mock.SetupGet(t => t.SportWithMatches).Returns(new List<Sport> { s1, s2 });

            mock.Setup(t => t.FilterDisabledMatches(It.IsAny<int>())).Returns(allMatches.AsQueryable());

            mock.Setup(t => t.FilterDisabledMatches(c1.Matches, It.IsAny<int>())).Returns(c1.Matches.AsEnumerable());
            mock.Setup(t => t.FilterDisabledMatches(c2.Matches, It.IsAny<int>())).Returns(c2.Matches.AsEnumerable());
            mock.Setup(t => t.FilterDisabledMatches(c3.Matches, It.IsAny<int>())).Returns(c3.Matches.AsEnumerable());
            mock.Setup(t => t.FilterDisabledMatches(c4.Matches, It.IsAny<int>())).Returns(c4.Matches.AsEnumerable());

            return mock;
        }
    }
}
