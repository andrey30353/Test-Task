namespace SportWebApi.Services
{
    public class SportDatabaseSettings : ISportDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public string SportMenuCollectionName { get; set; }
        public string EventDetailsCollectionName { get; set; }
        public string UpcomingEventsCollectionName { get; set; }
    }

    public interface ISportDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

        string SportMenuCollectionName { get; set; }
        string EventDetailsCollectionName { get; set; }
        string UpcomingEventsCollectionName { get; set; }
    }
}
