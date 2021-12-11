using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using product_service.Domain;
using product_service.Infrastructure.Utils;

namespace product_service.Infrastructure.Db
{
    public class InMemoryProductProvider : IProductProvider
    {
        private readonly Dictionary<string, Product> _products = new();
        private readonly FakeIdGenerator _idGenerator = new();
        private readonly FakeIdGenerator _versionIdGenerator = new();

        public Task<ICollection<Product>> GetAllProducts(bool onlyActive)
        {
            if (onlyActive)
            {
                return Task.FromResult<ICollection<Product>>(_products.Values.Where(product => product.isActive()).ToList());
            }
            return Task.FromResult<ICollection<Product>>(_products.Values);
        }
        
        public Task<Product> GetVersion(ProductId id, DateTime timestamp)
        {
            var product = _products.Values.ToList().Find(product => product.ProductId == id && product.versionActiveAt(timestamp));
            if (product == null)
            {
                throw new ProductNotFoundException("Product with id " + id.Raw + " not found");
            }

            return Task.FromResult(product);
        }

        public Task<Product> CreateProduct(Product product)
        {
            var newProduct = new Product(
                product.VersionId ?? ProductVersionId.Of(_versionIdGenerator.GenerateId()), 
                product.ProductId ?? ProductId.Of(_idGenerator.GenerateId()),
                product.Name,
                product.Description,
                product.Attributes,
                product.Version,
                product.ActiveTo,
                product.Stock,
                product.Price
            );

            _products[newProduct.VersionId.Raw] = newProduct;
            return Task.FromResult(newProduct);
        }

        public Task<Product> UpdateProduct(Product product)
        {
            var newProduct = new Product(
                product.VersionId,
                product.ProductId,
                product.Name,
                product.Description,
                product.Attributes,
                product.Version,
                product.ActiveTo,
                product.Stock,
                product.Price
            );

            _products[product.VersionId.Raw] = newProduct;
            return Task.FromResult(newProduct);
        }
    }
}
