using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_service.Domain;
using product_service.Infrastructure.Api.Requests;
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
        public IActionResult GetProducts([FromQuery] bool onlyActive = true)
        {
            return Ok(ProductMapper.ToDto(_productService.GetProducts(onlyActive)));
        }

        [HttpGet]
        [Route("/{productId}")]
        public IActionResult GetProductVersion(string productId, [FromQuery] string? version)
        {
            return Ok(ProductMapper.ToDto(_productService.GetProductVersion(
                ProductId.Of(productId),
                version == null ? DateTime.Now : DateTime.Parse(version))
            ));
        }

        [HttpPost]
        [Route("/{productId}")]
        public IActionResult UpdateProduct(string productId, [FromBody] UpdateProductRequest request)
        {
            return Ok(ProductMapper.ToDto(_productService.UpdateProduct(ProductId.Of(productId), request)));
        }

        [HttpDelete]
        [Route("/{productId}")]
        public IActionResult DeactivateProduct(string productId)
        {
            _productService.DeactivateProduct(ProductId.Of(productId));
            return Ok();
        }

        [HttpPost]
        [Route("/{productId}/stock")]
        public IActionResult DecreaseStock(string productId, [FromBody] StockDecreaseRequest request)
        {
            return Ok(_productService.DecreaseStock(ProductId.Of(productId), request.Amount));
        }
        
        [HttpPost]
        public IActionResult CreateProduct(NewProductRequest request)
        {
            return Ok(ProductMapper.ToDto(_productService.CreateProduct(request)));
        }
    }
}
