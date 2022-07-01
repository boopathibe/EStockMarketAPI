using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Models.Stock
{
    public class StockRequest
    {
        [JsonProperty("companyCode")]
        [Required]
        public string CompanyCode { get; set; }

        [JsonProperty("stockPrice")]
        [Required]
        public decimal StockPrice { get; set; }
    }
}
