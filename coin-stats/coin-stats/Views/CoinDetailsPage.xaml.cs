using coin_stats.Models.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coin_stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoinDetailsPage : ContentPage
    {
        private readonly Coin _coin;
        
        public CoinDetailsPage(Coin coin)
        {
            InitializeComponent();
            _coin = coin;
        }

        protected override void OnAppearing()
        {
            BindingContext = _coin;
        }
    }
}