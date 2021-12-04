using System;
using System.Collections.Generic;

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
        
        public Product(ProductId id, string name, DateTime version, int stock)
        {
            Id = id;
            Name = name;
            Version = version;
            Stock = stock;
        }
    }
}
