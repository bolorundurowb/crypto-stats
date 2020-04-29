using System;
using System.Diagnostics;
using coin_stats.Services;
using coin_stats.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace coin_stats
{
    public partial class App : Application
    {
        private static Stopwatch _stopWatch = new Stopwatch();
        private const int TimespanInMinutes = 5;
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CoinsPage())
            {
                BarTextColor = Color.FromHex("#E19832")
            };
        }

        protected override void OnStart()
        {
            // when the app starts, check if the stopwatch is running. If it isn't then start it
            if (!_stopWatch.IsRunning)
            {
                _stopWatch.Start();
            }

            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                if (_stopWatch.IsRunning && _stopWatch.Elapsed.Minutes >= TimespanInMinutes)
                {
                    // pull latest data
                    var service = new CoinStatsService();

                    // Perform your long running operations here.

                    Device.BeginInvokeOnMainThread(()=>{
                        // If you need to do anything with your UI, you need to wrap it in this.
                    });

                    _stopWatch.Restart();
                }

                // return true as to keep the timer running.
                return true;
            });
        }

        protected override void OnSleep()
        {
            _stopWatch.Reset();
        }

        protected override void OnResume()
        {
            _stopWatch.Start();
        }

        protected override void CleanUp()
        {
            _stopWatch.Stop();
            base.CleanUp();
        }
    }
}
