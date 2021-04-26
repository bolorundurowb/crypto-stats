using System;
using crypto_stats.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace crypto_stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            // set the picker details
            cmbTheme.ItemsSource = Constants.ThemeOptions;
            cmbRefreshFreq.ItemsSource = Constants.RefreshFrequencies;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cmbTheme.SelectedIndex = GetCurrentThemeIndex();
            cmbRefreshFreq.SelectedIndex = SyncManager.GetIndexOfSyncSelection();
            
            // set the version details
            lblAppVersion.Text = AppInfo.VersionString;
        }

        private void ChangeTheme(object sender, EventArgs e)
        {
            if (GetCurrentThemeIndex() != cmbTheme.SelectedIndex)
            {
                var theme = (ThemeManager.Themes) (cmbTheme.SelectedIndex);
                ThemeManager.ChangeTheme(theme);
                Toasts.DisplaySuccess($"{theme.ToString()} theme set successfully.");
            }
        }

        private void ChangeRefreshFrequency(object sender, EventArgs e)
        {
            var freq = Constants.RefreshFrequencies[cmbRefreshFreq.SelectedIndex];
            SyncManager.PersistSelection(freq);
        }

        private int GetCurrentThemeIndex()
        {
            var currentThemeIndex = (int) ThemeManager.CurrentTheme();
            return currentThemeIndex;
        }
    }
}
