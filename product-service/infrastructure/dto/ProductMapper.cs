using product_service.Domain;

namespace product_service.infrastructure.dto
{
    public class ProductMapper
    {
        public static ProductDto ToDto(Product product)
        {
            return new ProductDto(product.Id.Raw, product.Name);
        }
    }
}