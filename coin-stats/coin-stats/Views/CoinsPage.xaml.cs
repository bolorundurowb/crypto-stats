using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coin_stats.Models.Data;
using coin_stats.Models.View;
using coin_stats.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coin_stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoinsPage : ContentPage
    {
        private readonly CoinStatsService _service = new CoinStatsService();
        private List<CoinViewModel> _cryptoStats = new List<CoinViewModel>();
        private List<CoinViewModel> _filteredCryptoStats = new List<CoinViewModel>();

        public CoinsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await LoadData();
            prgLoading.IsVisible = false;
            lstCryptoStats.IsVisible = true;
        }

        protected async void OnRefresh(object sender, EventArgs e)
        {
            await LoadData();

            var search = txtSearch.Text?.ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(search))
            {
                SearchData(search);
            }

            lstCryptoStats.IsRefreshing = false;
        }

        protected void SearchStats(object sender, TextChangedEventArgs e)
        {
            var search = e.NewTextValue.ToLowerInvariant();
            SearchData(search);
        }

        protected async void ViewCoinDetails(object sender, ItemTappedEventArgs e)
        {
            var coin = e.Item as Coin;
            await Navigation.PushAsync(new CoinDetailsPage(coin));
        }

        private async Task LoadData()
        {
            var coins = await _service.GetAllStats();
            var vm = coins.Data
                .Select(x => new CoinViewModel(x))
                .ToList();
            _cryptoStats = vm;
            _filteredCryptoStats = vm;
            BindDataToUI();
        }

        private void SearchData(string query)
        {
            _filteredCryptoStats = _cryptoStats
                .Where(x => x.Coin.Id.ToLowerInvariant().Contains(query)
                            || x.Coin.Symbol.ToLowerInvariant().Contains(query)
                            || x.Coin.Name.ToLowerInvariant().Contains(query))
                .ToList();
            BindDataToUI();
        }

        private void BindDataToUI()
        {
            lstCryptoStats.ItemsSource = _filteredCryptoStats;
        }
    }
}