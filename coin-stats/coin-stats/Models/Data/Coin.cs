using System;
using Xamarin.Forms;

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

        public decimal PriceInUsd
        {
            get
            {
                decimal.TryParse(PriceUsd, out var price);
                return Math.Round(price, 2);
            }
        }

        public double LastDayChange
        {
            get
            {
                double.TryParse(ChangePercent24Hr, out var change);
                return Math.Round(change, 2);
            }
        }

        public Color LastDayColor
        {
            get
            {
                if (LastDayChange < 0)
                {
                    return Color.Red;
                }

                if (LastDayChange > 0)
                {
                    return Color.Green;
                }

                return Color.White;
            }
        }
    }
}