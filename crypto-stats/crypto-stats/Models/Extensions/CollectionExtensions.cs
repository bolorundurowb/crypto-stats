using System.Collections.Generic;
using System.Linq;
using crypto_stats.Models.Data;

namespace crypto_stats.Models.Extensions
{
    internal static class CollectionExtensions
    {
        public static IEnumerable<PricePoint> GetOrdered(this DataCollection<PricePoint> data)
        {
            return data.Data
                .OrderBy(x => x.Time)
                .ToList();
        }
    }
}
