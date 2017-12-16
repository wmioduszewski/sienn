namespace SIENN.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DbAccess.Persistance;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Services.Model;
    using Services.Resources;

    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly SiennDbContext context;
        private readonly IMapper mapper;

        public ProductsController(SiennDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ProductResource> GetProducts()
        {
            var products = context.Products.Include(p => p.Categories).ToList();
            return mapper.Map<List<Product>, List<ProductResource>>(products);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductResource productResource)
        {
            var type = context.Type.Find(productResource.TypeId);
            if (type == null)
            {
                ModelState.AddModelError("TypeId", "You're trying to assign product to not existing type");
            }

            var unit = context.Unit.Find(productResource.UnitId);
            if (unit == null)
            {
                ModelState.AddModelError("UnitId", "You're trying to assign product to not existing unit");
            }

            foreach (var categoryId in productResource.Categories)
            {
                if (context.Categories.Find(categoryId) == null)
                {
                    ModelState.AddModelError("CategoryId", "You're trying to assign product to not existing category");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = mapper.Map<ProductResource, Product>(productResource);

            context.Products.Add(product);
            context.SaveChanges();

            var result = mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductResource productResource)
        {
            var type = context.Type.Find(productResource.TypeId);
            if (type == null)
            {
                ModelState.AddModelError("TypeId", "You're trying to assign product to not existing type");
            }

            var unit = context.Unit.Find(productResource.UnitId);
            if (unit == null)
            {
                ModelState.AddModelError("UnitId", "You're trying to assign product to not existing unit");
            }

            foreach (var categoryId in productResource.Categories)
            {
                if (context.Categories.Find(categoryId) == null)
                {
                    ModelState.AddModelError("CategoryId", "You're trying to assign product to not existing category");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = context.Products.Find(id);
            mapper.Map(productResource, product);
            context.Products.Attach(product);
            context.SaveChanges();
            var result = mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            context.Remove(product);
            context.SaveChanges();

            return Ok(id);
        }
    }
}