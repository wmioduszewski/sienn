﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.DbAccess.Repositories
{
    using Services.Model;
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetAvailableProducts(ProductQuery productQuery);
        IEnumerable<Product> GetAll(Filter filter);
        Product Get(int id, bool includeRelated = true);
    }
}
