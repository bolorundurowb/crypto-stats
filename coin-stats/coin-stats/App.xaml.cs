﻿using coin_stats.Utils;
using coin_stats.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("FASolid.otf", Alias = "FAS")]
namespace coin_stats
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