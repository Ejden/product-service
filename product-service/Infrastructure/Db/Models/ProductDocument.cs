using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace product_service.Infrastructure.Db.Models
{
    public class ProductDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        #nullable enable
        public string? VersionId { get; set; }
        #nullable disable
        
        public string ProductId { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public ICollection<AttributeDocument> Attributes { get; set; }
        
        public DateTime Version { get; set; }
        
        public DateTime? ActiveTo { get; set; }
        
        public int Stock { get; set; }
        
        public MoneyDocument Price { get; set; }

        public ProductDocument() {}

        #nullable enable
        public ProductDocument(string? versionId, string productId, string name, string description, ICollection<AttributeDocument> attributes, DateTime version, DateTime? activeTo, int stock, MoneyDocument price)
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
