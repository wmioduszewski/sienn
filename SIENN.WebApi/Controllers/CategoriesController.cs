namespace SIENN.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DbAccess.Persistance;
    using Microsoft.AspNetCore.Mvc;
    using Services.Model;
    using Services.Resources;

    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly SiennDbContext context;
        private readonly IMapper mapper;

        public CategoriesController(SiennDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CategoryResource> GetCategories()
        {
            var categories = context.Categories.ToList();
            return mapper.Map<List<Category>, List<CategoryResource>>(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = mapper.Map<CategoryResource, Category>(categoryResource);

            context.Categories.Add(category);
            context.SaveChanges();

            var result = mapper.Map<Category, CategoryResource>(category);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = context.Categories.Find(id);
            mapper.Map<CategoryResource, Category>(categoryResource);

            context.SaveChanges();
            var result = mapper.Map<Category, CategoryResource>(category);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            context.Remove(category);
            context.SaveChanges();

            return Ok(id);
        }
    }
}