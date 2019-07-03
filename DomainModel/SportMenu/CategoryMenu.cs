using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace DomainModel
{
    public class CategoryMenu
    {
        [BsonElement("Name")]
        public string CategoryName { get; set; }

        public int MatchCount { get; set; }

        public List<string> Mathces { get; set; }
    }
}
