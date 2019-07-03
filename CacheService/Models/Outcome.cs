namespace CacheService.Models
{
    public class Outcome
    {
        public int OutcomeId { get; set; }

        public int MarketId { get; set; }
        public Market Market { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
