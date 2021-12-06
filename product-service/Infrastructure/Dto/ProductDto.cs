﻿using System;
using System.Collections.Generic;

namespace product_service.Infrastructure.Dto
{
    public record ProductDto(
        string Id, 
        string Name,
        string Description,
        List<AttributeDto> Attributes,
        DateTime Version,
        int Stock,
        MoneyDto Price
    );
}
