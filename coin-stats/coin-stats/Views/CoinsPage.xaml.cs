using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coin_stats.Models.Data;
using coin_stats.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coin_stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoinsPage : ContentPage
    {
        private readonly CoinStatsService _service = new CoinStatsService();
        private List<Coin> _cryptoStats = new List<Coin>();

        public CoinsPage()
        {
            InitializeComponent();
        }

        #region LifeCycle Overrides

        protected override async void OnAppearing()
        {
            await LoadData();
            prgLoading.IsVisible = false;
            lstCryptoStats.IsVisible = true;
        }

        #endregion

        #region Event Handlers

        private async void OnRefresh(object sender, EventArgs e)
        {
            await LoadData();

            var search = txtSearch.Text?.ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(search))
            {
                SearchData(search);
            }

            lstCryptoStats.IsRefreshing = false;
        }

        private void SearchStats(object sender, TextChangedEventArgs e)
        {
            var search = e.NewTextValue.ToLowerInvariant();
            SearchData(search);
        }

        private async void ViewCoinDetails(object sender, ItemTappedEventArgs e)
        {
            var coin = e.Item as Coin;
            await Navigation.PushAsync(new CoinDetailsPage(coin));
        }

        #endregion

        #region Helper Methods

        private async Task LoadData()
        {
            var coins = await _service.GetAllStats();
            _cryptoStats = coins.Data;
            BindDataToUi(coins.Data);
        }

        private void SearchData(string query)
        {
            var filteredCryptoStats = _cryptoStats
                .Where(x => x.Id.ToLowerInvariant().Contains(query)
                            || x.Symbol.ToLowerInvariant().Contains(query)
                            || x.Name.ToLowerInvariant().Contains(query))
                .ToList();
            BindDataToUi(filteredCryptoStats);
        }

        private void BindDataToUi(IEnumerable<Coin> data)
        {
            lstCryptoStats.ItemsSource = data;
        }

        #endregion
    }
}