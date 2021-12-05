using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_service.Domain;
using product_service.Infrastructure.Dto;

namespace product_service.Infrastructure.Api
{
    [ApiController]
    [Route("products")]
    public class ProductEndpoint : ControllerBase
    {

        private readonly ILogger<ProductEndpoint> _logger;
        private readonly ProductService _productService;

        public ProductEndpoint(ILogger<ProductEndpoint> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(ProductMapper.ToDto(_productService.GetProducts()));
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _productService.CreateProduct(product);
            return Ok();
        }
    }
}
