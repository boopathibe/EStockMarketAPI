using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Models.Stock
{
    public class StockDetails
    {
        [JsonProperty("companyCode")]
        public string CompanyCode { get; set; }

        [JsonProperty("stockPrice")]
        public decimal StockPrice { get; set; }

        [JsonProperty("stockDate")]
        public string StockDate { get; set; }

        [JsonProperty("stockTime")]
        public string StockTime { get; set; }

    }
}
