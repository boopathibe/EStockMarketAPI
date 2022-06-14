using Newtonsoft.Json;

namespace CompanyMicroService.Models
{
    public class CompanyResponseModel
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
