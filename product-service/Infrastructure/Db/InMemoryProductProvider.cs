using System;
using System.Collections.Generic;
using product_service.Domain;
using product_service.Infrastructure.Utils;

namespace product_service.Infrastructure.Db
{
    public class InMemoryProductProvider : IProductProvider
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly FakeIdGenerator _idGenerator = new FakeIdGenerator();

        public ICollection<Product> GetAllProducts()
        {
            return _products.AsReadOnly();
        }
        
        public Product GetVersion(ProductId id, DateTime timestamp)
        {
            throw new NotImplementedException();
        }

        public Product Save(Product product)
        {
            var newProduct = new Product(
                ProductId.Of(_idGenerator.GenerateId()),
                product.Name,
                product.Description,
                product.Attributes,
                DateTime.Now,
                product.Stock,
                product.Price
            );
            _products.Add(newProduct);
            return newProduct;
        }
    }
}
