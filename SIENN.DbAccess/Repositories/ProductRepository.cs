using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.DbAccess.Repositories
{
    using System.Linq;
    using Extensions;
    using Microsoft.EntityFrameworkCore;
    using Persistance;
    using Services.Model;
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(SiennDbContext context) : base(context)
        {
        }

        public override Product Get(int id)
        {
            return Get(id, LoadLevel.Categories);
        }

        public Product Get(int id, LoadLevel loadLevel)
        {
            //avoid eager loading of related objects when not needed
            switch (loadLevel)
            {
                case LoadLevel.Categories: return Context.Products
                    .Include(p => p.Categories)
                    .SingleOrDefault(p => p.Id == id);
                case LoadLevel.Full: return Context.Products
                    .Include(p => p.Categories)
                    .Include(p => p.Type)
                    .Include(p => p.Unit)
                    .SingleOrDefault(p => p.Id == id);
                //Basic
                default: return Context.Products.Find(id);
            }
        }

        public override IEnumerable<Product> GetAll()
        {
            return GetAll(new Filter());
        }

        public IEnumerable<Product> GetAll(Filter filter)
        {
            var products = Context.Products.Include(p => p.Categories).AsQueryable();
            if (filter.CategoryId.HasValue)
                products = products.Where(x => x.Categories.Any(y => y.CategoryId == filter.CategoryId.Value));
            if (filter.TypeId.HasValue)
                products = products.Where(x => x.TypeId == filter.TypeId.Value);
            if(filter.UnitId.HasValue)
                products = products.Where(x => x.UnitId == filter.UnitId.Value);
            return products.AsEnumerable();
        }

        public IEnumerable<Product> GetAvailableProducts(ProductQuery productQuery)
        {
            return Context.Products.Where(x => x.IsAvailable).Include(p => p.Categories).ApplyPaging(productQuery).AsEnumerable();
        }
    }
}
