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
            Console.WriteLine("DEBUG " + props.Value.ConnectionString + " " + props.Value.DatabaseName + " " + props.Value.ProductsCollectionName);
        }

        public ICollection<Product> GetAllProducts(bool onlyActive)
        {
            return onlyActive ? GetActiveProducts() : GetAllVersionsOfProducts();
        }

        public async Task<List<ProductDocument>> Get()
        {
            return await _productCollection.Find(_ => true).ToListAsync();
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
            return _productCollection
                .Find(it => it.ProductId == id.Raw && it.VersionActiveAt(timestamp))
                .Project(it => _modelMapper.ToDomain(it))
                .Single();
        }

        public void Save(Product product)
        { 
            _productCollection.InsertOne(_modelMapper.ToDocument(product));
        }
    }
}
