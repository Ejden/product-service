namespace product_service.Domain
{
    public record ProductId(string Raw)
    {
        public static ProductId Of(string raw)
        {
            return new ProductId(raw);
        }
    }
}
