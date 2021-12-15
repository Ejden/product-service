using System;

namespace product_service.Domain
{
    public record ProductId(string Raw)
    {
        public static ProductId Of(string raw)
        {
            return new ProductId(raw);
        }

        public override int GetHashCode()
        {
            return Raw.GetHashCode();
        }

        public override string ToString()
        {
            return Raw;
        }

        #nullable enable
        public virtual bool Equals(ProductId? other)
        {
            if (other == null) return false;
            return Raw == other.Raw;
        }
        #nullable disable
    }
}
