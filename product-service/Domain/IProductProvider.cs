using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using product_service.Infrastructure.Db.Models;

namespace product_service.Domain
{
    public interface IProductProvider
    {
        public ICollection<Product> GetAllProducts(bool onlyActive);
        
        public Product GetVersion(ProductId id, DateTime timestamp);

        public Task<List<ProductDocument>> Get();

        public void Save(Product product);
    }
}
