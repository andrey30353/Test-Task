using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DomainModel
{
    public class UpcomingEvents
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int ConfigurationId { get; set; }

        public string ConfigurationName { get; set; }

        public List<UpcomingEventsItem> UpcomingEventsList { get; set; }
    }
}