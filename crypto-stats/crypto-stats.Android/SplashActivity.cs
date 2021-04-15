using Android.App;
using Android.Content;

namespace crypto_stats.Android
{
    [Activity(Label = "Crypto Stats", Icon = "@mipmap/ic_launcher", Theme = "@style/Splash", MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed()
        {
        }
    }
}