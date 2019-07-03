using System.Collections.Generic;
using DomainModel;
using MongoDB.Driver;

namespace SportWebApi.Services
{
    public class SportService
    {
        private readonly IMongoCollection<SportMenu> _sportMenu;
        private readonly IMongoCollection<EventDetails> _eventDetails;
        private readonly IMongoCollection<UpcomingEvents> _upcomingEvents;

        public SportService(ISportDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _sportMenu = database.GetCollection<SportMenu>(settings.SportMenuCollectionName);
            _eventDetails = database.GetCollection<EventDetails>(settings.EventDetailsCollectionName);
            _upcomingEvents = database.GetCollection<UpcomingEvents>(settings.UpcomingEventsCollectionName);
        }

        public List<SportMenu> GetSportMenu() =>
            _sportMenu.Find(book => true).ToList();

        public SportMenu GetSportMenu(int clientId) =>
            _sportMenu.Find(book => book.ConfigurationId == clientId).FirstOrDefault();

        public List<EventDetails> GetEventDetails() =>
            _eventDetails.Find(book => true).ToList();

        public EventDetails GetEventDetails(int clientId) =>
            _eventDetails.Find(book => book.ConfigurationId == clientId).FirstOrDefault();

        public List<UpcomingEvents> GetUpcomingEvents() =>
            _upcomingEvents.Find(book => true).ToList();

        public UpcomingEvents GetUpcomingEvents(int clientId) =>
            _upcomingEvents.Find(book => book.ConfigurationId == clientId).FirstOrDefault();
    }
}
