using System.Collections.Generic;

namespace DomainModel
{
    public class EventDetailMarket
    {
        public string Name { get; set; }

        public List<EventDetailOutcome> Outcomes { get; set; }
    }
}