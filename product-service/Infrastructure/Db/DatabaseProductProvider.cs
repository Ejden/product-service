using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using product_service.Domain;
using product_service.Infrastructure.Db.Config;
using product_service.Infrastructure.Db.Models;

namespace product_service.Infrastructure.Db
{
    public class DatabaseProductProvider : IProductProvider
    {
        private readonly IMongoCollection<ProductDocument> _productCollection;
        
        public DatabaseProductProvider(IOptions<ProductServiceDatabaseProperties> props)
        {
            var mongoClient = new MongoClient(props.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(props.Value.DatabaseName);
            _productCollection = mongoDb.GetCollection<ProductDocument>(props.Value.ProductsCollectionName);
        }

        public async Task<ICollection<Product>> GetAllProducts(bool onlyActive)
        {
            return onlyActive ? await GetActiveProducts() : await GetAllVersionsOfProducts();
        }

        private async Task<ICollection<Product>> GetActiveProducts()
        {
            var now = DateTime.Now;

            var result = await _productCollection
                .FindAsync(it => now >= it.Version && (it.ActiveTo == null || now < it.ActiveTo));

            return result.ToList().Select(ModelMapper.ToDomain).ToList();
        }

        private async Task<ICollection<Product>> GetAllVersionsOfProducts()
        {
            var result = await _productCollection
                .FindAsync(_ => true);

            return result.ToList().Select(ModelMapper.ToDomain).ToList();
        }
        
        public async Task<Product> GetVersion(ProductId id, DateTime timestamp)
        {
            try
            {
                var result = await _productCollection
                    .FindAsync(it => it.ProductId == id.Raw && timestamp >= it.Version &&
                                     (it.ActiveTo == null || timestamp < it.ActiveTo));

                return ModelMapper.ToDomain(result.First());
            }
            catch (InvalidOperationException)
            {
                throw new ProductNotFoundException($"Product with id {id.Raw} not found");
            }
        }

        public async Task<Product> Create(Product product)
        {
            var productToSave = ModelMapper.ToDocument(product);
            await _productCollection.InsertOneAsync(productToSave);
            return await GetVersion(ProductId.Of(productToSave.ProductId), productToSave.Version);
        }

        public async Task<Product> Update(Product product)
        {
            var productToSave = ModelMapper.ToDocument(product);
            await _productCollection.ReplaceOneAsync(it => it.VersionId == product.VersionId.Raw, productToSave);
            return await GetVersion(ProductId.Of(productToSave.ProductId), product.Version);
        }
    }
}
