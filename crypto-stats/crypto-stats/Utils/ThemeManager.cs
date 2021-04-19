using crypto_stats.ThemeResources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace crypto_stats.Utils
{
    public static class ThemeManager
    {
        public enum Themes
        {
            Light,
            Dark
        }

        public static void ChangeTheme(Themes theme)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries == null)
            {
                return;
            }
            
            mergedDictionaries.Clear();
            Preferences.Set("SelectedTheme", (int) theme);

            switch (theme)
            {
                case Themes.Light:
                {
                    mergedDictionaries.Add(new LightTheme());
                    break;
                }
                case Themes.Dark:
                {
                    mergedDictionaries.Add(new DarkTheme());
                    break;
                }
                default:
                    mergedDictionaries.Add(new DarkTheme());
                    break;
            }
        }

        public static void LoadTheme()
        {
            var currentTheme = CurrentTheme();
            ChangeTheme(currentTheme);
        }

        public static Themes CurrentTheme()
        {
            return (Themes) Preferences.Get("SelectedTheme", (int) Themes.Dark);
        }
    }
}
