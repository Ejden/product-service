using System.Collections;
using System.Collections.Generic;

namespace product_service.Domain
{
    public class Attributes<T> : List<Attribute<T>>
    {

        public Attributes(List<Attribute<T>> attributes)
        {
            attributes.ForEach(Add);
        }

        public Attributes() {}
        
        public IEnumerable GetAttributes()
        {
            return AsReadOnly();
        }
    }

    public record Attribute<T>(string Key, T Value);
}
