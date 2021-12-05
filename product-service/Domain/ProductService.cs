using System.Collections.Generic;

namespace product_service.Domain
{
    public class ProductService
    {
        private readonly IProductProvider _productProvider;
        //
        public ProductService(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        public ICollection<Product> GetProducts()
        {
            return _productProvider.GetAllProducts();
        }

        public void CreateProduct(Product product)
        {
            _productProvider.Save(product);
        }
    }
}
