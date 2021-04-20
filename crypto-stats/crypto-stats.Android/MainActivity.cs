using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using crypto_stats.Utils;

namespace crypto_stats.Android
{
    [Activity(Label = "Crypto Stats", Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            var theme = ThemeManager.CurrentTheme();
            switch (theme)
            {
                case ThemeManager.Themes.Light:
                {
                    SetTheme(Resource.Style.lightAppTheme);
                    break;
                }
                default:
                {
                    SetTheme(Resource.Style.darkAppTheme);
                    break;
                }
            }

            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());

            // set bottom bar colour
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.SetNavigationBarColor(
                    theme == ThemeManager.Themes.Dark ? Color.Black : new Color(175, 175, 175));
            }
        }
    }
}
