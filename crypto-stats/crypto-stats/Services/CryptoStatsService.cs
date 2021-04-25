using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using crypto_stats.Models.Data;
using crypto_stats.Models.Extensions;
using crypto_stats.Utils;
using crypto_stats.Utils.Extensions;
using Polly;

namespace crypto_stats.Services
{
    public class CryptoStatsService
    {
        private const string ApiUrl = "https://api.coincap.io/v2/assets";
        private static readonly HttpClient Client = new HttpClient();

        private static readonly JsonSerializerOptions DeserializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<DataCollection<Crypto>> GetAllStatsAsync()
        {
            return await PullDataWithRetriesAsync<DataCollection<Crypto>>(ApiUrl);
        }

        public async Task<DataCollection<PricePoint>> GetExtendedHistoryAsync(string id,
            int intervalInMinutes = 24 * 60)
        {
            var endDate = DateTime.Now;
            var startDate = endDate - TimeSpan.FromMinutes(intervalInMinutes);
            return await PullDataWithRetriesAsync<DataCollection<PricePoint>>(
                $"https://api.coincap.io/v2/assets/{id}/history?interval=h2&start={startDate.ToUnixTimeStamp()}&end={endDate.ToUnixTimeStamp()}");
        }

        private static async Task<T> PullDataAsync<T>(string url)
        {
            var response = await Client.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<T>(response, DeserializerOptions);
        }

        private static async Task<T> PullDataWithRetriesAsync<T>(string url)
        {
            return await Policy
                .Handle<Exception>(x => x.IsInternetConnectionException())
                .WaitAndRetryAsync
                (
                    3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) => { Toasts.DisplayError("An error occurred while retrieving data. Retrying..."); }
                )
                .ExecuteAsync(async () => await PullDataAsync<T>(url));
        }
    }
}
