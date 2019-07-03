using System.Collections.Generic;

namespace DomainModel
{
    public class SportMenuItem
    {
        public string SportName { get; set; }

        public int MatchCount { get; set; }

        public List<CategoryMenu> Categories { get; set; }
    }
}
