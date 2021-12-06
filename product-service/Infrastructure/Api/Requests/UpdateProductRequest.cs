using System.Collections.Generic;

namespace product_service.Infrastructure.Api.Requests
{
    public record UpdateProductRequest(
        string Name,
        string Description,
        List<AttributeRequest> Attributes,
        int Stock,
        MoneyRequest Price
    );
}
