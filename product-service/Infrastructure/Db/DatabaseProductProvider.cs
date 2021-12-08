using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly ModelMapper _modelMapper;
        
        public DatabaseProductProvider(
            IOptions<ProductServiceDatabaseProperties> props,
            ModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
            var mongoClient = new MongoClient(props.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(props.Value.DatabaseName);
            _productCollection = mongoDb.GetCollection<ProductDocument>(props.Value.ProductsCollectionName);
        }

        public ICollection<Product> GetAllProducts(bool onlyActive)
        {
            return onlyActive ? GetActiveProducts() : GetAllVersionsOfProducts();
        }

        private ICollection<Product> GetActiveProducts()
        {
            var now = DateTime.Now;
            return _productCollection
                .Find(it => now >= it.Version && (it.ActiveTo == null || now < it.ActiveTo))
                .Project(it => _modelMapper.ToDomain(it))
                .ToList();
        }

        private ICollection<Product> GetAllVersionsOfProducts()
        {
            return _productCollection
                .Find(_ => true)
                .Project(it => _modelMapper.ToDomain(it))
                .ToList();
        }
        
        public Product GetVersion(ProductId id, DateTime timestamp)
        {
            try
            {
                return _productCollection
                    .Find(it => it.ProductId == id.Raw && timestamp >= it.Version &&
                                (it.ActiveTo == null || timestamp < it.ActiveTo))
                    .Project(it => _modelMapper.ToDomain(it))
                    .Single();
            }
            catch (InvalidOperationException)
            {
                throw new ProductNotFoundException($"Product with id {id.Raw} not found");
            }
        }

        public Product Insert(Product product)
        {
            var productToSave = _modelMapper.ToDocument(product);
            _productCollection.InsertOne(productToSave);
            return GetVersion(ProductId.Of(productToSave.ProductId), productToSave.Version);
        }

        public Product Update(Product product)
        {
            var productToSave = _modelMapper.ToDocument(product);
            _productCollection.ReplaceOne(it => it.VersionId == product.VersionId.Raw, productToSave);
            return GetVersion(ProductId.Of(productToSave.ProductId), product.Version);
        }
    }
}
