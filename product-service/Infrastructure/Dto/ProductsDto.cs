using System.Collections.Generic;

namespace product_service.Infrastructure.Dto
{
    public record ProductsDto(List<ProductDto> products);
}
