namespace coin_stats.Models.Data
{
    public class Coin
    {
        public string Id { get; set; }

        public string Rank { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public string PriceUsd { get; set; }

        public string ChangePercent24Hr { get; set; }

        public decimal PriceInUsd()
        {
            decimal.TryParse(PriceUsd, out var price);
            return price;
        }

        public double LastDayChange()
        {
            double.TryParse(ChangePercent24Hr, out var change);
            return change;
        }
    }
}