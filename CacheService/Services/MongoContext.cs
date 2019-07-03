using System.Collections.Generic;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CacheService.Services
{
    public class MongoContext : DbContext
    {
        private readonly string _sportMenuCollectionName = "SportMenu";
        private readonly string _eventDetailsCollectionName = "EventDetails";
        private readonly string _upcomingEventsCollectionName = "UpcomingEvents";

        private readonly IMongoDatabase _database;

        private readonly IMongoCollection<SportMenu> _sportMenu;
        private readonly IMongoCollection<EventDetails> _eventDetails;
        private readonly IMongoCollection<UpcomingEvents> _upcomingEvents;

        public MongoContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("SportDb");
            _sportMenu = _database.GetCollection<SportMenu>(_sportMenuCollectionName);
            _eventDetails = _database.GetCollection<EventDetails>(_eventDetailsCollectionName);
            _upcomingEvents = _database.GetCollection<UpcomingEvents>(_upcomingEventsCollectionName);
        }

        public void ReplaceAll(List<SportMenu> sportMenu)
        {
            _database.DropCollection(_sportMenuCollectionName);

            if(sportMenu.Count != 0)
                _sportMenu.InsertMany(sportMenu);
        }

        public void ReplaceAll(List<EventDetails> eventDetails)
        {
            _database.DropCollection(_eventDetailsCollectionName);

            if (eventDetails.Count != 0)
                _eventDetails.InsertMany(eventDetails);
        }

        public void ReplaceAll(List<UpcomingEvents> upcomingEvents)
        {
            _database.DropCollection(_upcomingEventsCollectionName);

            if (upcomingEvents.Count != 0)
                _upcomingEvents.InsertMany(upcomingEvents);
        }
    }
}
