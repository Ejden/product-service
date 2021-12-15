using System.Collections.Generic;
using System.Linq;
using product_service.Domain;

namespace product_service.Infrastructure.Api.Dto
{
    public class ProductMapper : IDomainToDtoMapper<Product, ProductDto>
    {
        public ProductDto ToDto(Product product)
        {
            return new ProductDto(
                product.ProductId.Raw,
                product.Name,
                product.Description, 
                ToDto(product.Attributes),
                product.Version,
                product.Stock,
                ToDto(product.Price)
            );
        }

        public ProductsDto ToDto(ICollection<Product> products)
        {
            return new ProductsDto(products.Select(ToDto).ToList());
        }

        private List<AttributeDto<string>> ToDto(Attributes<string> attributes)
        {
            return attributes.Select(attribute => new AttributeDto<string>(attribute.Key, attribute.Value)).ToList();
        }

        private static MoneyDto ToDto(Money money)
        {
            return new MoneyDto(money.Amount, money.Currency.ToString());
        }
    }

    public interface IDomainToDtoMapper<in T, out TR>
    {
        public TR ToDto(T t);
    }
}
