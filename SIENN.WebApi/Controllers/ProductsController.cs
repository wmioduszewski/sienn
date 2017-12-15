using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SIENN.WebApi.Controllers
{
    using System.Data.SqlClient;
    using AutoMapper;
    using DbAccess.Persistance;
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
            var products = context.Products.Include(p=>p.Categories).ToList();
            return mapper.Map<List<Product>, List<ProductResource>>(products);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = mapper.Map<ProductResource, Product>(productResource);

            try
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return NotFound("Unit, type or category couldn't be found\n" + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

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