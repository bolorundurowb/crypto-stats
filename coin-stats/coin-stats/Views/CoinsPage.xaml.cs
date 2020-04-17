using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coin_stats.Models.Data;
using coin_stats.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coin_stats
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoinsPage : ContentPage
    {
        private readonly CoinStatsService _service = new CoinStatsService();
        private List<Coin> _cryptoStats = new List<Coin>();
        private List<Coin> _filteredCryptoStats = new List<Coin>();

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

        private async Task LoadData()
        {
            var coins = await _service.GetAllStats();
            _cryptoStats = coins.Data;
            _filteredCryptoStats = coins.Data;
            BindDataToUI();
        }

        private void SearchData(string query)
        {
            _filteredCryptoStats = _cryptoStats
                .Where(x => x.Id.ToLowerInvariant().Contains(query)
                            || x.Symbol.ToLowerInvariant().Contains(query)
                            || x.Name.ToLowerInvariant().Contains(query))
                .ToList();
            BindDataToUI();
        }

        private void BindDataToUI()
        {
            lstCryptoStats.ItemsSource = _filteredCryptoStats;
        }
    }
}