using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using crypto_stats.Models.Data;
using crypto_stats.Utils.Extensions;

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
            var response = await Client.GetStreamAsync(ApiUrl);
            return await JsonSerializer.DeserializeAsync<DataCollection<Crypto>>(response, DeserializerOptions);
        }

        public async Task<DataCollection<PricePoint>> GetHistoryAsync(string id)
        {
            var response = await Client.GetStreamAsync($"https://api.coincap.io/v2/assets/{id}/history?interval=m1");
            return await JsonSerializer.DeserializeAsync<DataCollection<PricePoint>>(response, DeserializerOptions);
        }

        public async Task<DataCollection<PricePoint>> GetExtendedHistoryAsync(string id, int intervalInMinutes = 24 * 60)
        {
            var endDate = DateTime.Now;
            var startDate = (endDate - TimeSpan.FromMinutes(intervalInMinutes));
            var start = startDate.ToUnixTimeStamp();
            var end = endDate.ToUnixTimeStamp();
            var response = await Client.GetStreamAsync($"https://api.coincap.io/v2/assets/{id}/history?interval=h6&start={start}&end={end}");
            return await JsonSerializer.DeserializeAsync<DataCollection<PricePoint>>(response, DeserializerOptions);
        }
    }
}
