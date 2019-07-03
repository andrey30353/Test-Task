using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace CacheService.Services
{
    public class DataMapService
    {
        private readonly IDataService _dataService;

        public DataMapService(MsSqlContext context)
        {           
            _dataService = new DataService(context);
        }

        public DataMapService(IDataService dataService)
        {            
            _dataService = dataService;
        }

        public List<SportMenu> CreateSportMenu()
        {
            var sportMenuList = new List<SportMenu>();
                        
            foreach (var config in _dataService.EnableConfigurations)
            {
                var sportMenu = new SportMenu
                {
                    ConfigurationId = config.ConfigurationId,
                    ConfigurationName = config.Name
                };

                var sports = _dataService.SportWithMatches;
                var sportList = new List<SportMenuItem>(sports.Count);
                foreach (var sport in sports)
                {
                    var spItem = new SportMenuItem()
                    {
                        SportName = sport.Name,
                    };

                    var matchCount = 0;
                    var sportCat = sport.Categories.ToList();
                    var categories = new List<CategoryMenu>(sportCat.Count);
                    foreach (var cat in sportCat)
                    {
                        var matches = _dataService.FilterDisabledMatches(cat.Matches, config.ConfigurationId)
                            .Select(t => t.Name).ToList();
                        var categoryMenu = new CategoryMenu
                        {
                            CategoryName = cat.Name,
                            Mathces = matches,
                            MatchCount = matches.Count
                        };
                        categories.Add(categoryMenu);
                        matchCount += matches.Count;
                    }

                    spItem.Categories = categories;
                    spItem.MatchCount = matchCount;
                    sportList.Add(spItem);
                }

                sportMenu.SportList = sportList;
                sportMenuList.Add(sportMenu);
            }
            return sportMenuList;
        }

        public List<EventDetails> CreateEventDetails()
        {
            var eventDetails = new List<EventDetails>();
            
            foreach (var config in _dataService.EnableConfigurations)
            {
                var details = new EventDetails
                {
                    ConfigurationId = config.ConfigurationId,
                    ConfigurationName = config.Name,
                };

                var enableMatches = _dataService.FilterDisabledMatches(config.ConfigurationId)
                    .Include(m => m.Markets)
                    .ThenInclude(mark => mark.Outcomes)
                    .Include(t => t.Category.Sport)
                    .ToList();

                var eventDetailsList = new List<EventDetailsItem>(enableMatches.Count);
                foreach (var match in enableMatches)
                {
                    var eventDetailsItem = new EventDetailsItem()
                    {
                        MatchName = match.Name,
                        MatchDate = match.MatchDate,
                        CategoryName = match.Category.Name,
                        SportName = match.Category.Sport.Name
                    };

                    var markets = match.Markets.ToList();
                    var marketsList = new List<EventDetailMarket>(markets.Count);
                    foreach (var market in markets)
                    {
                        var sportMargin = _dataService.GetSportMargin(config.ConfigurationId, match.Category.SportId);

                        var newMarket = new EventDetailMarket();
                        newMarket.Name = market.Name;
                        newMarket.Outcomes = market.Outcomes
                            .Select(t => new EventDetailOutcome()
                            {
                                Name = t.Name,
                                Price = t.Price * sportMargin
                            }).ToList();
                        marketsList.Add(newMarket);
                    }

                    eventDetailsItem.Markets = marketsList;
                    eventDetailsList.Add(eventDetailsItem);
                }

                details.EventDetailsList = eventDetailsList;
                eventDetails.Add(details);
            }

            return eventDetails;
        }

        public List<UpcomingEvents> CreateUpcomingEvents()
        {
            var upcomingEvents = new List<UpcomingEvents>();
          
            foreach (var config in _dataService.EnableConfigurations)
            {
                var details = new UpcomingEvents
                {
                    ConfigurationId = config.ConfigurationId,
                    ConfigurationName = config.Name,
                };

                var enableMatches = _dataService.FilterDisabledMatches(config.ConfigurationId)
                    .Where(t => t.MatchDate < DateTime.Now.AddDays(1))
                    .ToList();
                var upcomingEventsList = new List<UpcomingEventsItem>(enableMatches.Count);
                foreach (var match in enableMatches)
                {
                    var eventDetailsItem = new UpcomingEventsItem()
                    {
                        MatchName = match.Name,
                        MatchDate = match.MatchDate
                    };
                    upcomingEventsList.Add(eventDetailsItem);
                }

                details.UpcomingEventsList = upcomingEventsList;
                upcomingEvents.Add(details);
            }


            return upcomingEvents;
        }
    }
}
