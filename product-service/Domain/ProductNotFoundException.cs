using System;

namespace product_service.Domain
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(ProductId id) : base($"Product with id {id} not found") { }
    }
}
