using MongoDB.Bson.Serialization.Attributes;

namespace product_service.Infrastructure.Db.Models
{
    public class AttributeDocument<T>
    {
        [BsonElement("key")]
        public string Key { get; set; }
        
        [BsonElement("value")]
        public T Value { get; set; }

        public AttributeDocument() {}

        public AttributeDocument(string key, T value)
        {
            Key = key;
            Value = value;
        }
    }
}