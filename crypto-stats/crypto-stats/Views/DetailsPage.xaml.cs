using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using crypto_stats.Models.Data;
using crypto_stats.Models.Extensions;
using crypto_stats.Services;
using crypto_stats.Utils;
using crypto_stats.Utils.Extensions;
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
        private readonly Crypto _crypto;

        public DetailsPage(Crypto crypto)
        {
            InitializeComponent();
            _crypto = crypto;
        }

        protected override async void OnAppearing()
        {
            BindingContext = _crypto;

            // instantiate and render chart details
            var chart = new LineChart
            {
                BackgroundColor =
                    SKColor.Parse(ThemeManager.CurrentTheme() == ThemeManager.Themes.Dark
                        ? Constants.DarkBackgroundColour
                        : Constants.LightBackgroundColour),
                LineSize = 5,
                PointSize = 7.5f,
            };
            chrtTrend.Chart = chart;

            // initialize the tab  state
            tabInterval.SelectedIndex = 0;
        }

        protected async void IntervalChanges(object sender, SelectedPositionChangedEventArgs e)
        {
            var selectedIndex = (int) e.SelectedPosition;
            await LoadHistoryForIndex(selectedIndex);
        }

        private async Task LoadHistoryForIndex(int selectedIndex)
        {
            DataCollection<PricePoint> entries = null;

            switch (selectedIndex)
            {
                case 0:
                    entries = await _service.GetFifteenMinHistoryAsync(_crypto.Id);
                    break;
                case 1:
                    entries = await _service.GetDaysHistoryAsync(_crypto.Id);
                    break;
                case 2:
                    entries = await _service.GetWeekHistoryAsync(_crypto.Id);
                    break;
            }

            WriteDataToChart(entries?.GetOrdered());
        }

        private void WriteDataToChart(IEnumerable<PricePoint> prices)
        {
            if (prices == null)
            {
                return;
            }

            var history = prices.ToList();
            var entries = history
                .Select((x, index) =>
                {
                    float.TryParse(x.PriceUsd, out var currentPrice);
                    var colour = SKColor.Parse(Constants.PositiveColour);

                    if (index > 0)
                    {
                        float.TryParse(history[index - 1].PriceUsd, out var previousPrice);

                        if (currentPrice < previousPrice)
                        {
                            colour = SKColor.Parse(Constants.NegativeColour);
                        }
                    }

                    return new ChartEntry(currentPrice)
                    {
                        Color = colour,
                        Label = x.Time.FromUnixTimeStamp().ToString("dd MMM, h:mm"),
                        ValueLabelColor = colour,
                        ValueLabel = currentPrice.ToString(CultureInfo.InvariantCulture)
                    };
                })
                .ToArray();

            chrtTrend.Chart.Entries = entries;
        }
    }
}
