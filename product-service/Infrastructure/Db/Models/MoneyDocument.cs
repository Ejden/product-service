using MongoDB.Bson.Serialization.Attributes;

namespace product_service.Infrastructure.Db.Models
{
    public class MoneyDocument
    {
        [BsonElement("amount")]
        public decimal Amount { get; set; }
        
        [BsonElement("currency")]
        public string Currency { get; set; }

        public MoneyDocument() {}

        public MoneyDocument(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}
