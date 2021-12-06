using System;
using System.Linq;
using product_service.Infrastructure.Api.Requests;

namespace product_service.Domain
{
    public class ProductFactory
    {
        public Product CreateProduct(NewProductRequest newProductRequest)
        {
            return new Product(
                null,
                null,
                newProductRequest.Name,
                newProductRequest.Description,
                new Attributes(newProductRequest.Attributes.Select(attr => new Attribute(attr.Key, attr.Value)).ToList()),
                DateTime.Now,
                newProductRequest.Stock,
                new Money(newProductRequest.Price.Amount, Enum.Parse<Currency>(newProductRequest.Price.Currency))
            );
        }

        public Product CreateProduct(Product product, UpdateProductRequest updateProductRequest)
        {
            return new Product(
                null,
                product.ProductId,
                updateProductRequest.Name,
                updateProductRequest.Description,
                new Attributes(updateProductRequest.Attributes.Select(attr => new Attribute(attr.Key, attr.Value)).ToList()),
                DateTime.Now,
                updateProductRequest.Stock,
                new Money(updateProductRequest.Price.Amount, Enum.Parse<Currency>(updateProductRequest.Price.Currency))
            );
        }
    }
}
