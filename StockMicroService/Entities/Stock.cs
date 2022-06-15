using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Entities
{
    [BsonIgnoreExtraElements]
    public class Stock
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("companyCode")]
        public string CompanyCode { get; set; }

        [BsonElement("stockPrice")]
        public decimal StockPrice { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
