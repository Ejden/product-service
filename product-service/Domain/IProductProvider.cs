﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace product_service.Domain
{
    public interface IProductProvider
    {
        public Task<ICollection<Product>> GetAllProducts(bool onlyActive);
        
        public Task<Product> GetVersion(ProductId id, DateTime timestamp);
        
        public Task<Product> Create(Product product);

        public Task<Product> Update(Product product);
    }
}
