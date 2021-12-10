using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace product_service.Infrastructure.Db.Models
{
    public class ProductDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("versionId")]
        #nullable enable
        public string? VersionId { get; set; }
        #nullable disable
        
        [BsonElement("productId")]
        public string ProductId { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("attributes")]
        public ICollection<AttributeDocument> Attributes { get; set; }
        
        [BsonElement("version")]
        public DateTime Version { get; set; }
        
        [BsonElement("activeTo")]
        public DateTime? ActiveTo { get; set; }
        
        [BsonElement("stock")]
        public int Stock { get; set; }
        
        [BsonElement("price")]
        public MoneyDocument Price { get; set; }

        public ProductDocument() {}

        #nullable enable
        public ProductDocument(string? versionId, string? productId, string name, string description, ICollection<AttributeDocument> attributes, DateTime version, DateTime? activeTo, int stock, MoneyDocument price)
        {
            VersionId = versionId;
            ProductId = productId;
            Name = name;
            Description = description;
            Attributes = attributes;
            Version = version;
            ActiveTo = activeTo;
            Stock = stock;
            Price = price;
        }
        #nullable disable
        
        public bool VersionActiveAt(DateTime timestamp)
        {
            return timestamp >= Version && (ActiveTo == null || timestamp < ActiveTo);
        }

        public bool IsActive()
        {
            return VersionActiveAt(DateTime.Now);
        }
    }
}
