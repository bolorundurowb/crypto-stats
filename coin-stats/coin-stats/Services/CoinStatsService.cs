using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using coin_stats.Models.Data;

namespace coin_stats.Services
{
    public class CoinStatsService
    {
        private const string ApiUrl = "https://api.coincap.io/v2/assets";
        private static readonly HttpClient Client = new HttpClient();

        private static readonly JsonSerializerOptions DeserializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Coins> GetAllStats()
        {
            var response = await Client.GetStreamAsync(ApiUrl);
            return await JsonSerializer.DeserializeAsync<Coins>(response, DeserializerOptions);
        }
    }
}