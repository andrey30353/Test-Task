using System.Collections.Generic;

namespace CacheService.Models
{
    public class Sport
    {
        public int SportId { get; set; }
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
