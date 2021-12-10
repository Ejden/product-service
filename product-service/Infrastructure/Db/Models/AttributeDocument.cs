using MongoDB.Bson.Serialization.Attributes;

namespace product_service.Infrastructure.Db.Models
{
    public class AttributeDocument
    {
        [BsonElement("key")]
        public string Key { get; set; }
        
        [BsonElement("value")]
        public string Value { get; set; }

        public AttributeDocument() {}

        public AttributeDocument(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}