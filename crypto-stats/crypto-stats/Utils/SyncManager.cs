using System.Linq;
using crypto_stats.Models.View;
using Xamarin.Essentials;

namespace crypto_stats.Utils
{
    public class SyncManager
    {
        private const string SyncFreqKey = "CryptoStats_SyncFrequency";

        internal static void PersistSelection(KeyValueViewModel vm)
        {
            Preferences.Set(SyncFreqKey, vm.Value);
        }

        internal static int GetSyncFrequencyInMinutes()
        {
            return Preferences.Get(SyncFreqKey, 5);
        }

        internal static int GetIndexOfSyncSelection()
        {
            var value = GetSyncFrequencyInMinutes();
            var selection = Constants.RefreshFrequencies
                .FirstOrDefault(x => x.Value == value);

            if (selection == null)
            {
                return -1;
            }

            return Constants.RefreshFrequencies
                .IndexOf(selection);
        }
    }
}
