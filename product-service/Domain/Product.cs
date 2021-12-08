using System;

namespace product_service.Domain
{
    public class Product
    {
        public ProductVersionId VersionId { get; }
        
        public ProductId ProductId { get; }
        
        public string Name { get; }
        
        public string Description { get; }
        
        public Attributes Attributes { get; }

        public DateTime Version { get; }
        
        public DateTime? ActiveTo { get; }

        public int Stock { get; }

        public Money Price { get; }
        
        public Product(ProductVersionId versionId, ProductId productId, string name, string description, Attributes attributes, DateTime version, int stock, Money price)
        {
            VersionId = versionId;
            ProductId = productId;
            Name = name;
            Description = description;
            Attributes = attributes;
            Version = version;
            ActiveTo = null;
            Stock = stock;
            Price = price;
        }
        
        public Product(ProductVersionId versionId, ProductId productId, string name, string description, Attributes attributes, DateTime version, DateTime? activeTo, int stock, Money price)
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

        public bool versionActiveAt(DateTime timestamp)
        {
            return timestamp >= Version && (ActiveTo == null || timestamp < ActiveTo);
        }

        public bool isActive()
        {
            return versionActiveAt(DateTime.Now);
        }
        
        public Product Deactivate()
        {
            return new Product(VersionId, ProductId, Name, Description, Attributes, Version, DateTime.Now, Stock, Price);
        }

        public Product Deactivate(DateTime endDate)
        {
            return new Product(VersionId, ProductId, Name, Description, Attributes, Version, endDate, Stock, Price);
        }

        public Product DecreaseStock(int amount)
        {
            if (amount > Stock || amount < 0)
            {
                throw new Exception("Can't decrease stock from " + Stock + " by " + amount);
            }

            return new Product(null, ProductId, Name, Description, Attributes, DateTime.Now, ActiveTo, Stock - amount, Price);
        }
    }
}
