using Xamarin.Forms;

namespace coin_stats.Models.Data
{
    public class Coin
    {
        public string Id { get; set; }

        public string Rank { get; set; }

        public string Symbol { get; set; }

        public string MaxSupply { get; set; }

        public string Supply { get; set; }

        public string MarketCapUsd { get; set; }

        public string VolumeUsd24Hr { get; set; }

        public string VWap24Hr { get; set; }

        public string Name { get; set; }

        public string PriceUsd { get; set; }

        public string ChangePercent24Hr { get; set; }

        public decimal PriceInUsd
        {
            get
            {
                decimal.TryParse(PriceUsd, out var price);
                return price;
            }
        }

        public decimal TwentyFourHourPercentageChange
        {
            get
            {
                decimal.TryParse(ChangePercent24Hr, out var change);
                return change;
            }
        }

        public Color TwentyFourHourChangeColour
        {
            get
            {
                if (TwentyFourHourPercentageChange < 0)
                {
                    return Color.Red;
                }

                return TwentyFourHourPercentageChange > 0 ? Color.FromHex("#0EDD7B") : Color.White;
            }
        }

        public decimal MaxCoinSupply
        {
            get
            {
                decimal.TryParse(MaxSupply, out var supply);
                return supply;
            }
        }

        public decimal TotalCoinSupply
        {
            get
            {
                decimal.TryParse(Supply, out var supply);
                return supply;
            }
        }

        public decimal MarketCapInUsd
        {
            get
            {
                decimal.TryParse(MarketCapUsd, out var marketCap);
                return marketCap;
            }
        }

        public decimal VolumeTradedLastTwentyFourHours
        {
            get
            {
                decimal.TryParse(VolumeUsd24Hr, out var volumeTraded);
                return volumeTraded;
            }
        }

        public decimal VolumeWeightedAveragePriceLastTwentyFourHours
        {
            get
            {
                decimal.TryParse(VWap24Hr, out var vwap);
                return vwap;
            }
        }
    }
}