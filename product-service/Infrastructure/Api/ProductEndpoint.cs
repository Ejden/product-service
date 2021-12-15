using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_service.Domain;
using product_service.Infrastructure.Api.Requests;
using product_service.Infrastructure.Api.Dto;

namespace product_service.Infrastructure.Api
{
    [ApiController]
    [Route("products")]
    public class ProductEndpoint : ControllerBase
    {

        private readonly ILogger<ProductEndpoint> _logger;
        
        private readonly ProductService _productService;

        private readonly ProductMapper _productMapper;

        public ProductEndpoint(ILogger<ProductEndpoint> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
            _productMapper = new ProductMapper();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] bool onlyActive = true)
        {
            return Ok(_productMapper.ToDto(await _productService.GetProducts(onlyActive)));
        }

        #nullable enable
        [HttpGet]
        [Route("{productId}")]
        public async Task<IActionResult> GetProductVersion(string productId, [FromQuery] string? version)
        {
            return Ok(_productMapper.ToDto(await _productService.GetProductVersion(
                ProductId.Of(productId),
                version == null ? DateTime.Now : DateTime.Parse(version))
            ));
        }
        #nullable disable

        [HttpPost]
        [Route("{productId}")]
        public async Task<IActionResult> UpdateProduct(string productId, [FromBody] UpdateProductRequest request)
        {
            return Ok(_productMapper.ToDto(await _productService.UpdateProduct(ProductId.Of(productId), request)));
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> DeactivateProduct(string productId)
        {
            await _productService.DeactivateProduct(ProductId.Of(productId));
            return Ok();
        }

        [HttpPost]
        [Route("{productId}/stock")]
        public async Task<IActionResult> DecreaseStock(string productId, [FromBody] StockDecreaseRequest request)
        {
            return Ok(_productMapper.ToDto(await _productService.DecreaseStock(ProductId.Of(productId), request.DecreaseBy)));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(NewProductRequest request)
        {
            return Ok(_productMapper.ToDto(await _productService.CreateProduct(request)));
        }
    }
}
