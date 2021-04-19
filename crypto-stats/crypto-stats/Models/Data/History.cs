using System.Collections.Generic;
using System.Linq;

namespace crypto_stats.Models.Data
{
    public class History
    {
        public List<PricePoint> Data { get; set; }

        public List<PricePoint> OrderedData
        {
            get
            {
                var data = Data ?? new List<PricePoint>();
                return data
                    .OrderBy(x => x.Time)
                    .ToList();
            }
        }
    }
}