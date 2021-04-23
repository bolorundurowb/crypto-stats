using System;
using System.Collections.Generic;
using System.Linq;
using crypto_stats.Models.View;
using crypto_stats.Utils;

namespace crypto_stats
{
    public static class Constants
    {
        public const string PositiveColour = "#0EDD7B";
        
        public const string NegativeColour = "#DC4035";
        
        public const string DarkBackgroundColour = "#000000";
        
        public const string LightBackgroundColour = "#FFFFFF";

        public static List<EnumViewModel> ThemeOptions = Enum.GetValues(typeof(ThemeManager.Themes))
            .Cast<ThemeManager.Themes>()
            .Select(x => new EnumViewModel(x))
            .OrderBy(x => x.Id)
            .ToList();

        public static List<KeyValueViewModel> RefreshFrequencies = new List<KeyValueViewModel>
        {
            new KeyValueViewModel(1, "Every Minute"),
            new KeyValueViewModel(2, "Every Other Minute"),
            new KeyValueViewModel(5, "Every Five Minutes"),
            new KeyValueViewModel(15, "Every Fifteen Minutes"),
            new KeyValueViewModel(30, "Every Half Hour"),
            new KeyValueViewModel(-1, "Never")
        };
    }
}
