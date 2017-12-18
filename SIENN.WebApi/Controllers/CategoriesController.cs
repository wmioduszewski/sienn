namespace SIENN.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DbAccess.Repositories;
    using DbAccess.UnitOfWork;
    using Microsoft.AspNetCore.Mvc;
    using Services.Model;
    using Services.Resources;

    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CategoriesController(IGenericRepository<Category> categoryRepository, IUnitOfWork uow, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CategoryResource> GetCategories()
        {
            var categories = categoryRepository.GetAll().ToList();
            return mapper.Map<List<Category>, List<CategoryResource>>(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryResource = mapper.Map<Category, CategoryResource>(category);
            return Ok(categoryResource);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = mapper.Map<CategoryResource, Category>(categoryResource);

            categoryRepository.Add(category);
            uow.Complete();

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

            var category = categoryRepository.Get(id);
            mapper.Map<CategoryResource, Category>(categoryResource);

            uow.Complete();
            var result = mapper.Map<Category, CategoryResource>(category);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            categoryRepository.Remove(category);
            uow.Complete();

            return Ok(id);
        }
    }
}