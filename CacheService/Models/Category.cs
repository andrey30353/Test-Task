using System.Collections.Generic;

namespace CacheService.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public string Name { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
