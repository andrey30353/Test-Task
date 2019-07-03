using CacheService.Models;

namespace CacheService.Customization
{
    public class ConfigurationSportMargin
    {
        public int ConfigurationId { get; set; }
       
        public int SportId { get; set; }
        public Sport Sport { get; set; }

        public double MarginValue { get; set; }
    }
}
