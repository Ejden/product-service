namespace product_service.Infrastructure.Db.Models
{
    public class AttributeDocument
    {
        public string Key { get; set; }
        
        public string Value { get; set; }

        public AttributeDocument() {}

        public AttributeDocument(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}