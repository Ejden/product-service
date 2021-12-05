using System;

namespace product_service.Domain
{
    public class Product
    {
        public ProductId Id { get; }
        
        public string Name { get; }
        
        public string Description { get; }
        
        public Attributes Attributes { get; }

        public DateTime Version { get; }

        public int Stock { get; }

        public Money Price { get; }
        
        public Product(ProductId id, string name, string description, Attributes attributes, DateTime version, int stock, Money price)
        {
            Id = id;
            Name = name;
            Description = description;
            Attributes = attributes;
            Version = version;
            Stock = stock;
            Price = price;
        }
    }
}
