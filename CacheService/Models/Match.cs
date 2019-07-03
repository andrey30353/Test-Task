using System;
using System.Collections.Generic;

namespace CacheService.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Name { get; set; }

        public DateTime MatchDate { get; set; }

        public ICollection<Market> Markets { get; set; }
    }
}
