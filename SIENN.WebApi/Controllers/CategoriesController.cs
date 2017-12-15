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
        public IEnumerable<CategoryResource> GetTypes()
        {
            var categories = context.Categories.ToList();
            return mapper.Map<List<Category>, List<CategoryResource>>(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryResource categoryResource)
        {
            var category = mapper.Map<CategoryResource, Category>(categoryResource);

            context.Categories.Add(category);
            context.SaveChanges();

            var result = mapper.Map<Category, CategoryResource>(category);

            return Ok(result);
        }
    }
}