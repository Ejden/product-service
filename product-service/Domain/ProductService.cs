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

        public async Task<ICollection<Product>> GetProducts(bool onlyActive)
        {
            return await _productProvider.GetAllProducts(onlyActive);
        }

        public async Task<Product> GetProductVersion(ProductId productId, DateTime version)
        {
            return await _productProvider.GetVersion(productId, version);
        }

        public async Task<Product> CreateProduct(NewProductRequest newProductRequest)
        {
            _productValidator.ValidateNewProductRequest(newProductRequest);
            
            var product = _productFactory.CreateProduct(newProductRequest);
            return await _productProvider.CreateProduct(product);
        }

        public async Task<Product> UpdateProduct(ProductId productId, UpdateProductRequest updateProductRequest)
        {
            var productToUpdate = await _productProvider.GetVersion(productId, DateTime.Now);
            var updatedProduct = _productFactory.CreateProduct(productToUpdate, updateProductRequest);
            var oldProductVersion = productToUpdate.Deactivate(updatedProduct.Version);
            await _productProvider.UpdateProduct(oldProductVersion);
            return await _productProvider.CreateProduct(updatedProduct);
        }

        public async Task<Product> DecreaseStock(ProductId productId, int amount)
        {
            var productToUpdate = await _productProvider.GetVersion(productId, DateTime.Now);
            
            _productValidator.ValidateStockDecrease(productToUpdate, amount);
            
            var updatedProduct = productToUpdate.DecreaseStock(amount);
            var oldProductVersion = productToUpdate.Deactivate(updatedProduct.Version);
            
            await _productProvider.UpdateProduct(oldProductVersion);
            return await _productProvider.CreateProduct(updatedProduct);
        }

        public async Task<Product> DeactivateProduct(ProductId productId)
        {
            var product = await _productProvider.GetVersion(productId, DateTime.Now);
            product = product.Deactivate();
            return await _productProvider.UpdateProduct(product);
        }
    }
}
