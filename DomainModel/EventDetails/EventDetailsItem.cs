using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class EventDetailsItem
    {
        public string MatchName { get; set; }

        public DateTime MatchDate { get; set; }

        public string CategoryName { get; set; }

        public string SportName { get; set; }

        public List<EventDetailMarket> Markets { get; set; }
    }
}