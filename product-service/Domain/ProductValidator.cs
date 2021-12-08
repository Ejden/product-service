using System;
using System.Collections.Generic;
using product_service.Infrastructure.Api.Requests;

namespace product_service.Domain
{
    public class ProductValidator
    {
        public void ValidateNewProductRequest(NewProductRequest request)
        {
            ValidateProductName(request.Name);
            ValidateProductDescription(request.Description);
            ValidateAttributes(request.Attributes);
            ValidateProductStock(request.Stock);
            ValidateProductPrice(request.Price);
        }

        public void ValidateStockDecrease(Product product, int decreaseBy)
        {
            ValidateThatProductIsActive(product);
            ValidateProductStock(product.Stock - decreaseBy);
        }

        private void ValidateThatProductIsActive(Product product)
        {
            if (!product.isActive())
            {
                throw new ValidationException("Product is inactive");
            }
        }

        private void ValidateProductName(string name)
        {
            ValidateStringLength(name);
        }

        private void ValidateProductDescription(string description)
        {
            ValidateStringLength(description);
        }
        
        private void ValidateAttributes(List<AttributeRequest> attributes)
        {
            attributes.ForEach(ValidateAttribute);
        }

        private void ValidateAttribute(AttributeRequest attributeRequest)
        {
            ValidateStringLength(attributeRequest.Key);
            ValidateStringLength(attributeRequest.Value);
        }

        private void ValidateProductStock(int stock)
        {
            if (stock < 0)
            {
                throw new ValidationException("Stock should be grater or equal 0");
            }
        }

        private void ValidateProductPrice(MoneyRequest price)
        {
            ValidatePriceAmount(price.Amount);
            ValidatePriceCurrency(price.Currency);
        }

        private void ValidatePriceAmount(decimal amount)
        {
            if (amount <= 0.00m)
            {
                throw new ValidationException("Price should be grater than 0.00");
            }
        }

        private void ValidatePriceCurrency(string currency)
        {
            var successful = Enum.TryParse(currency, out Currency ca);
            if (!successful)
            {
                throw new ValidationException("Unsupported currency");
            }
        }
        
        private void ValidateStringLength(string arg)
        {
            if (arg.Trim().Length <= 0)
            {
                throw new ValidationException("Text length should be grater than 0");
            }
        }
    }
}
