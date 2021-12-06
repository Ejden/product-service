using System;

namespace product_service.Domain
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
