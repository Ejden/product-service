using System;

namespace product_service.Domain
{
    public interface IProductProvider
    {
        public Product GetVersion(ProductId id, DateTime timestamp);

        public Product Save(Product product);
    }
}