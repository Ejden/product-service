using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using product_service.Domain;
using product_service.infrastructure.dto;

namespace product_service.infrastructure.api
{
    [ApiController]
    [Route("products")]
    public class ProductEndpoint : Controller
    {
        private static readonly Product[] Products =
        {
            new Product(ProductId.Of("product-1"), "Iphone 11"), 
            new Product(ProductId.Of("product-2"), "Ipad Pro")
        };

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(Products.Select(ProductMapper.ToDto));
        }
    }
}