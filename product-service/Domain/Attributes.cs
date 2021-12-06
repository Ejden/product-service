using System.Collections;
using System.Collections.Generic;

namespace product_service.Domain
{
    public class Attributes : List<Attribute>
    {

        public Attributes(List<Attribute> attributes)
        {
            attributes.ForEach(Add);
        }

        public Attributes() {}
        
        public IEnumerable GetAttributes()
        {
            return AsReadOnly();
        }
    }

    public record Attribute(string Key, string Value);
}
