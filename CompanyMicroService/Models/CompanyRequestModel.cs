using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Models
{
    public class CompanyRequestModel
    {
        [JsonProperty("code")]
        [Required]
        public string Code { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("ceoName")]
        [Required]
        public string CeoName { get; set; }

        [JsonProperty("turnOver")]
        [Required]
        [Range(100000000, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}")]
        public decimal TurnOver { get; set; }

        [JsonProperty("website")]
        [Required]
       //// [RegularExpression(@"^(http|http(s)?://)?([\w-]+\.)+[\w-]+[.com|.in|.org]+(\[\?%&=]*)?", ErrorMessage ="Invalid website")]
        public string Website { get; set; }

        [JsonProperty("exchange")]
        [Required]
        public string[] Exchange { get; set; }
    }
}
