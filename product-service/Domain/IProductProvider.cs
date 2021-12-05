using System;
using System.Collections.Generic;

namespace product_service.Domain
{
    public interface IProductProvider
    {
        public ICollection<Product> GetAllProducts();
        
        public Product GetVersion(ProductId id, DateTime timestamp);

        public Product Save(Product product);
    }
}