using System;
using System.Collections;
using System.Collections.Generic;

namespace product_service.Domain
{
    public class Attributes : List<Attribute>
    {
        public IEnumerable GetAttributes()
        {
            return AsReadOnly();
        }
    }

    public record Attribute(string Key, string Value);
}
