using System.Linq;
using crypto_stats.Models.Data;
using crypto_stats.Services;
using crypto_stats.Utils;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace crypto_stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        private readonly CryptoStatsService _service = new CryptoStatsService();
        private readonly Coin _coin;

        public DetailsPage(Coin coin)
        {
            InitializeComponent();
            _coin = coin;
        }

        protected override async void OnAppearing()
        {
            BindingContext = _coin;

            // pull and display chart
            var extendedHistory = await _service.GetExtendedHistoryAsync(_coin.Id);
            var entries = extendedHistory.OrderedData
                .Select(x =>
                {
                    float.TryParse(x.PriceUsd, out var price);
                    return new ChartEntry(price)
                    {
                        Color = SKColor.Parse(Constants.PositiveColour)
                    };
                })
                .ToArray();

            var chart = new LineChart
            {
                Entries = entries,
                BackgroundColor =
                    SKColor.Parse(ThemeManager.CurrentTheme() == ThemeManager.Themes.Dark
                        ? Constants.DarkBackgroundColour
                        : Constants.LightBackgroundColour),
                LineSize = 6,
                PointSize = 10
            };
            chrtTrend.Chart = chart;

            // display the UI
            prgLoadingChart.IsVisible = false;
            chrtTrend.IsVisible = true;
        }
    }
}
