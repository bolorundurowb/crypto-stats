using coin_stats.ThemeResources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace coin_stats.Utils
{
    public class ThemeManager
    {
        public enum Themes
        {
            Light,
            Dark
        }

        public static void ChangeTheme(Themes theme)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
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
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
        }

        public static void LoadTheme()
        {
            Themes currentTheme = CurrentTheme();
            ChangeTheme(currentTheme);
        }

        public static Themes CurrentTheme()
        {
            return (Themes) Preferences.Get("SelectedTheme", (int) Themes.Dark);
        }
    }
}