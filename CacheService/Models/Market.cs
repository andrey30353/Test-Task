using System.Collections.Generic;

namespace CacheService.Models
{
    public class Market
    {
        public int MarketId { get; set; }

        public int MatchId { get; set; }
        public Match Match { get; set; }

        public string Name { get; set; }

        public ICollection<Outcome> Outcomes { get; set; }
    }
}
