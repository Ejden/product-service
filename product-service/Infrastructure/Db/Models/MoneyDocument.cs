namespace product_service.Infrastructure.Db.Models
{
    public class MoneyDocument
    {
        public decimal Amount { get; set; }
        
        public string Currency { get; set; }

        public MoneyDocument() {}

        public MoneyDocument(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}