using crypto_stats.Utils;
using crypto_stats.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("FASolid.otf", Alias = "FAS")]
[assembly: ExportFont("GorditaBold.otf", Alias = "GorditaBold")]
[assembly: ExportFont("Gordita.otf", Alias = "Gordita")]
namespace crypto_stats
{
    public partial class App : Application
    {
        public App()
        {
            // set experimental flags
            Device.SetFlags(new []
            {
                "Expander_Experimental"
            });
            
            InitializeComponent();

            // apply theme
            ThemeManager.LoadTheme();

            MainPage = new NavigationPage(new CoinsPage())
            {
                BarTextColor = Color.FromHex("#E19832")
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            CoinsPage.StopBackgroundRefresh();
        }

        protected override void OnResume()
        {
        }
    }
}
