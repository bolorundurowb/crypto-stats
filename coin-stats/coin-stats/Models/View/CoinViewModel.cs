﻿using System.Linq;
using System.Threading.Tasks;
using coin_stats.Models.Data;
using coin_stats.Services;
using Microcharts;
using SkiaSharp;

namespace coin_stats.Models.View
{
    public class CoinViewModel
    {
        public Coin Coin { get; set; }

        public Chart History { get; private set; }

        public async Task LoadHistory()
        {
            var history = await (new CoinStatsService()).GetHistory(Coin?.Id);
            var entries = history.OrderedData
                .Select(x =>
                {
                    float.TryParse(x.PriceUsd, out var price);
                    return new Microcharts.Entry(price)
                    {
                        Color = SKColor.Parse(Constants.PositiveColour)
                    };
                })
                .ToArray();

            var chart = new LineChart
            {
                Entries = entries,
                BackgroundColor = SKColor.Parse(Constants.BackgroundColour),
                LineSize = 6,
                PointSize = 10
            };
            History = chart;
        }
    }
}