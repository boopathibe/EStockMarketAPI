using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Models.Stock
{
    public class StockResponse
    {
        [JsonProperty("stocks")]
        public List<StockDetails> Stocks { get; set; }

        [JsonProperty("maxPrice")]
        public decimal? MaxPrice { get; set; }

        [JsonProperty("minPrice")]
        public decimal? MinPrice { get; set; }

        [JsonProperty("avgPrice")]
        public decimal? AvgPrice { get; set; }
    }
}
