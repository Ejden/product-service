using System;
using System.Collections.Generic;

namespace product_service.Infrastructure.Dto
{
    public record ProductDto(
        string id, 
        string name,
        string description,
        List<AttributeDto> attributes,
        DateTime version,
        int stock,
        MoneyDto price
    );
}
