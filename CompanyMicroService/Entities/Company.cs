using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Entities
{
    [BsonIgnoreExtraElements]
    public class Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("ceoName")]
        public string CeoName { get; set; }

        [BsonElement("turnOver")]
        public decimal TurnOver { get; set; }

        [BsonElement("website")]
        public string Website { get; set; }

        [BsonElement("exchange")]
        public string[] Exchange { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

}