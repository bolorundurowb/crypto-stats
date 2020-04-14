namespace coin_stats.Models.Data
{
    public class Coin
    {
        public string Id { get; set; }
        
        public string Rank { get; set; }
        
        public string Symbol { get; set; }
        
        public string Name { get; set; }

        public decimal PriceUsd { get; set; }

        public double ChangePercent24Hr { get; set; }
    }
}