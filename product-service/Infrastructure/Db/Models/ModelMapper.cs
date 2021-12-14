using System;
using System.Collections.Generic;
using System.Linq;
using product_service.Domain;

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

        private static Attributes<string> ToDomain(ICollection<AttributeDocument<string>> attributes)
        {
            return new Attributes<string>(attributes.Select(attribute => new Attribute<string>(attribute.Key, attribute.Value)).ToList());
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

        private static ICollection<AttributeDocument<string>> ToDocument(Attributes<string> attributes)
        {
            return attributes.GetAttributes().OfType<Attribute<string>>()
                .Select(it => new AttributeDocument<string>(it.Key, it.Value))
                .ToList();
        }

        private static MoneyDocument ToDocument(Money money)
        {
            return new MoneyDocument(money.Amount, money.Currency.ToString());
        }
    }
}
