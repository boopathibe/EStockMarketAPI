using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Models
{
    public class CompanyResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ceoName")]
        public string CeoName { get; set; }

        [JsonProperty("turnOver")]
        public decimal TurnOver { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("exchange")]
        public string[] Exchange { get; set; }
    
    }
}
