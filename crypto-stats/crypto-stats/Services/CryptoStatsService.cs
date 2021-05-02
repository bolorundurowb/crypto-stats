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

        public  Task<DataCollection<PricePoint>> GetFifteenMinHistoryAsync(string assetId)
        {
            return GetAssetHistoryWithIntervals(assetId, TimeSpan.FromMinutes(15), "m1");
        }

        public  Task<DataCollection<PricePoint>> GetDaysHistoryAsync(string assetId)
        {
            return GetAssetHistoryWithIntervals(assetId, TimeSpan.FromDays(1), "h1");
        }

        public  Task<DataCollection<PricePoint>> GetWeekHistoryAsync(string assetId)
        {
            return GetAssetHistoryWithIntervals(assetId, TimeSpan.FromDays(7), "h6");
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
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)),
                    (ex, time) => { Toasts.DisplayError("An error occurred while retrieving data. Retrying..."); }
                )
                .ExecuteAsync(async () => await PullDataAsync<T>(url));
        }

        private async Task<DataCollection<PricePoint>> GetAssetHistoryWithIntervals(string assetId, TimeSpan period, string interval)
        {
            var endDate = DateTime.Now;
            var startDate = endDate - period;
            return await PullDataWithRetriesAsync<DataCollection<PricePoint>>(
                $"https://api.coincap.io/v2/assets/{assetId}/history?interval={interval}&start={startDate.ToUnixTimeStamp()}&end={endDate.ToUnixTimeStamp()}");
        }
    }
}
