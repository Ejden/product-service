using System.Collections.Generic;
using System.Linq;
using product_service.Domain;

namespace product_service.Infrastructure.Dto
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product product)
        {
            return new ProductDto(
                product.Id.Raw,
                product.Name,
                product.Description, 
                product.Attributes.ToDto(),
                product.Version,
                product.Stock,
                product.Price.ToDto()
            );
        }

        public static ProductsDto ToDto(ICollection<Product> products)
        {
            return new ProductsDto(products.Select(ToDto).ToList());
        }

        private static List<AttributeDto> ToDto(this Attributes attributes)
        {
            return attributes.Select(attribute => new AttributeDto(attribute.Key, attribute.Value)).ToList();
        }

        private static MoneyDto ToDto(this Money money)
        {
            return new MoneyDto(money.Amount, money.Currency.ToString());
        }
    }
}
