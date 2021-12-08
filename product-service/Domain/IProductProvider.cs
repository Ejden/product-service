using System;
using System.Collections.Generic;

namespace product_service.Domain
{
    public interface IProductProvider
    {
        public ICollection<Product> GetAllProducts(bool onlyActive);
        
        public Product GetVersion(ProductId id, DateTime timestamp);
        
        public Product Create(Product product);

        public Product Update(Product product);
    }
}
