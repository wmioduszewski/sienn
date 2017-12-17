using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.DbAccess.Repositories
{
    using System.Linq;
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
            return Get(id, true);
        }

        public Product Get(int id, bool includeRelated)
        {
            //avoid eager loading of related objects when not needed
            if (!includeRelated)
                return Context.Products.Find(id);

            return Context.Products.Include(p => p.Categories).SingleOrDefault(p => p.Id == id);
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

        public IEnumerable<Product> GetAvailableProducts()
        {
            return GetAll().Where(x => x.IsAvailable).AsEnumerable();
        }
    }
}
