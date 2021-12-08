using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using product_service.Infrastructure.Api.Requests;
using product_service.Infrastructure.Db.Models;

namespace product_service.Domain
{
    public class ProductService
    {
        private readonly IProductProvider _productProvider;
        
        private readonly ProductValidator _productValidator;
        
        private readonly ProductFactory _productFactory;
        
        public ProductService(IProductProvider productProvider, ProductValidator productValidator, ProductFactory productFactory)
        {
            _productProvider = productProvider;
            _productValidator = productValidator;
            _productFactory = productFactory;
        }

        public ICollection<Product> GetProducts(bool onlyActive)
        {
            return _productProvider.GetAllProducts(onlyActive);
        }

        public Product GetProductVersion(ProductId productId, DateTime version)
        {
            return _productProvider.GetVersion(productId, version);
        }

        public void CreateProduct(NewProductRequest newProductRequest)
        {
            _productValidator.ValidateNewProductRequest(newProductRequest);
            var product = _productFactory.CreateProduct(newProductRequest);
            // return _productProvider.Save(product);
        }

        public async Task<List<ProductDocument>> Get()
        {
            return await _productProvider.Get();
        }

        public void UpdateProduct(ProductId productId, UpdateProductRequest updateProductRequest)
        {
            var productToUpdate = _productProvider.GetVersion(productId, DateTime.Now);
            var updatedProduct = _productFactory.CreateProduct(productToUpdate, updateProductRequest);
            var oldProductVersion = productToUpdate.Deactivate(updatedProduct.Version);
            _productProvider.Save(oldProductVersion);
            // return _productProvider.Save(updatedProduct);
        }

        public void DecreaseStock(ProductId productId, int amount)
        {
            var productToUpdate = _productProvider.GetVersion(productId, DateTime.Now);
            _productValidator.ValidateStockDecrease(productToUpdate, amount);
            var updatedProduct = productToUpdate.DecreaseStock(amount);
            var oldProductVersion = productToUpdate.Deactivate(updatedProduct.Version);
            _productProvider.Save(oldProductVersion);
            // return _productProvider.Save(updatedProduct);
        }

        public void DeactivateProduct(ProductId productId)
        {
            var product = _productProvider.GetVersion(productId, DateTime.Now);
            product = product.Deactivate();
            _productProvider.Save(product);
        }
    }
}
