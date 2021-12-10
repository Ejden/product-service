using System;
using System.Collections.Generic;
using System.Linq;
using product_service.Domain;
using Attribute = product_service.Domain.Attribute;

namespace product_service.Infrastructure.Db.Models
{
    public abstract class ModelMapper
    {
        public static Product ToDomain(ProductDocument productDocument)
        {
            return new Product(
                versionId: new ProductVersionId(productDocument.VersionId),
                productId: new ProductId(productDocument.ProductId),
                name: productDocument.Name,
                description: productDocument.Description,
                attributes: ToDomain(productDocument.Attributes),
                version: productDocument.Version,
                activeTo: productDocument.ActiveTo,
                stock: productDocument.Stock,
                price: ToDomain(productDocument.Price)
            );
        }

        private static Attributes ToDomain(ICollection<AttributeDocument> attributes)
        {
            return new Attributes(attributes.Select(attribute => new Attribute(attribute.Key, attribute.Value)).ToList());
        }

        private static Money ToDomain(MoneyDocument money)
        {
            return new Money(money.Amount, Enum.Parse<Currency>(money.Currency));
        }

        public static ProductDocument ToDocument(Product product)
        {
            return new ProductDocument(
                versionId: product.VersionId?.Raw ?? Guid.NewGuid().ToString(),
                productId: product.ProductId?.Raw ?? Guid.NewGuid().ToString(),
                name: product.Name,
                description: product.Description,
                attributes: ToDocument(product.Attributes),
                version: product.Version,
                activeTo: product.ActiveTo,
                stock: product.Stock,
                price: ToDocument(product.Price)
            );
        }

        private static ICollection<AttributeDocument> ToDocument(Attributes attributes)
        {
            return attributes.GetAttributes().OfType<Attribute>()
                .Select(it => new AttributeDocument(it.Key, it.Value))
                .ToList();
        }

        private static MoneyDocument ToDocument(Money money)
        {
            return new MoneyDocument(money.Amount, money.Currency.ToString());
        }
    }
}
