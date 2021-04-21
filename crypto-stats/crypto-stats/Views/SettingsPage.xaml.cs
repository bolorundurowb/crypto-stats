using System;
using System.Linq;
using crypto_stats.Models.View;
using crypto_stats.Utils;
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
            cmbTheme.ItemsSource = Enum.GetValues(typeof(ThemeManager.Themes))
                .Cast<ThemeManager.Themes>()
                .Select(x => new EnumViewModel(x))
                .OrderBy(x => x.Id)
                .ToList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cmbTheme.SelectedIndex = GetCurrentThemeIndex();
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

        private int GetCurrentThemeIndex()
        {
            var currentThemeIndex = (int) ThemeManager.CurrentTheme();
            return currentThemeIndex;
        }
    }
}
