using System;
using System.Threading.Tasks;
using coin_stats.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coin_stats
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoinsPage : ContentPage
    {
        private readonly CoinStatsService _service = new CoinStatsService();

        public CoinsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await LoadData();
            // set UI
            prgLoading.IsVisible = false;
            lstCryptoStats.IsVisible = true;
        }

        protected async void OnRefresh(object sender, EventArgs e)
        {
            await LoadData();
            lstCryptoStats.IsRefreshing = false;
        }

        private async Task LoadData()
        {
            var coins = await _service.GetAllStats();
            lstCryptoStats.ItemsSource = coins.Data;
        }
    }
}