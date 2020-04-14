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
            var coins = await _service.GetAllStats();
            lstCryptoStats.ItemsSource = coins.Data;
            
            // set UI
            prgLoading.IsVisible = false;
            lstCryptoStats.IsVisible = true;
        }
    }
}