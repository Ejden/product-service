using System.Collections.Generic;

namespace product_service.Infrastructure.Api.Requests
{
    public record NewProductRequest(
        string Name,
        string Description,
        List<AttributeRequest> Attributes,
        int Stock,
        MoneyRequest Price
    );

    public record AttributeRequest(string Key, string Value);

    public record MoneyRequest(decimal Amount, string Currency);
}
