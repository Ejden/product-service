using System;
using System.Collections.Generic;
using product_service.Domain;

namespace product_service.Infrastructure.Db
{
    public class DatabaseProductProvider : IProductProvider
    {
        public ICollection<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }
        
        public Product GetVersion(ProductId id, DateTime timestamp)
        {
            throw new NotImplementedException();
        }

        public Product Save(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
