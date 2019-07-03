using CacheService.Models;

namespace CacheService.Customization
{
    public class ConfigurationMatchDisabled
    {
        public int ConfigurationId { get; set; }
        
        public int MatchId { get; set; }

        public Match Match { get; set; }
    }
}
