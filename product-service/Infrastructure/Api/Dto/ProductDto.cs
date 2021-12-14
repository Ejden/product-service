using System;
using System.Collections.Generic;

namespace product_service.Infrastructure.Api.Dto
{
    public record ProductDto(
        string Id, 
        string Name,
        string Description,
        List<AttributeDto<string>> Attributes,
        DateTime Version,
        int Stock,
        MoneyDto Price
    );
}
