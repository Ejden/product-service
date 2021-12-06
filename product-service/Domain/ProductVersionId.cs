namespace product_service.Domain
{
    public record ProductVersionId(string Raw)
    {
        public static ProductVersionId Of(string raw)
        {
            return new ProductVersionId(raw);
        }
    }
}
