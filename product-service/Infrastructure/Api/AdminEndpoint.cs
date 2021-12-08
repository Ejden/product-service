using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using product_service.Domain;
using product_service.Infrastructure.Api.Requests;

namespace product_service.Infrastructure.Api
{
    [ApiController]
    [Route("admin")]
    public class AdminEndpoint : ControllerBase
    {

        private readonly ProductService _productService;

        public AdminEndpoint(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("/generate-default-products")]
        public IActionResult GenerateDefaultProducts()
        {
            _productService.CreateProduct(new NewProductRequest(
                "PearPhone 11",
                "Super telefon",
                new List<AttributeRequest> { new("color", "Czarny") },
                50,
                new MoneyRequest(2500.25m, "PLN")
            ));
            _productService.CreateProduct(new NewProductRequest(
                "PearPad 5",
                "Super tablet",
                new List<AttributeRequest> { new("color", "Czarny") },
                111,
                new MoneyRequest(1500.25m, "PLN")
            ));
            _productService.CreateProduct(new NewProductRequest(
                "PearBook Pro",
                "Super telefon",
                new List<AttributeRequest> { new("color", "Czarny") },
                1,
                new MoneyRequest(2500.25m, "PLN")
            ));
            return Ok();
        }
    }
}