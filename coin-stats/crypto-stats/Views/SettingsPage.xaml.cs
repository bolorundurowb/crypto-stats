using System;
using System.Linq;
using coin_stats.Models.View;
using coin_stats.Utils;
using Plugin.Toast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coin_stats.Views
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

            cmbTheme.SelectedIndex = GetCurrentThemeIndex();
        }

        private void ChangeTheme(object sender, EventArgs e)
        {
            if (GetCurrentThemeIndex() != cmbTheme.SelectedIndex)
            {
                var theme = (ThemeManager.Themes) (cmbTheme.SelectedIndex);
                ThemeManager.ChangeTheme(theme);
                CrossToastPopUp.Current.ShowCustomToast($"{theme.ToString()} theme set successfully.", "#E19832",
                    "#000000");
            }
        }

        private int GetCurrentThemeIndex()
        {
            var currentThemeIndex = (int) ThemeManager.CurrentTheme();
            return currentThemeIndex;
        }
    }
}