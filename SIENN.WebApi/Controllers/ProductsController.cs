namespace SIENN.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DbAccess;
    using DbAccess.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Services.Model;
    using Services.Resources;

    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IGenericRepository<Type> typeRepository;
        private readonly IGenericRepository<Unit> unitRepository;
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository productRepository, IGenericRepository<Type> typeRepository, 
            IGenericRepository<Unit> unitRepository, IGenericRepository<Category> categoryRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.typeRepository = typeRepository;
            this.unitRepository = unitRepository;
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ProductResource> GetProducts(FilterResource filterResource)
        {
            Filter filter = mapper.Map<FilterResource, Filter>(filterResource);
            var products = productRepository.GetAll(filter).ToList();
            return mapper.Map<List<Product>, List<ProductResource>>(products);
        }

        [HttpGet("available")]
        public IEnumerable<ProductResource> GetAvailableProducts(ProductQuery productQuery)
        {
            var products = productRepository.GetAvailableProducts(productQuery).ToList();
            return mapper.Map<List<Product>, List<ProductResource>>(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = productRepository.Get(id, LoadLevel.Full);
            if (product == null)
            {
                return NotFound();
            }
            var productResource = mapper.Map<Product, SiennProductResource>(product);
            return Ok(productResource);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductResource productResource)
        {
            var type = typeRepository.Get(productResource.TypeId);
            if (type == null)
            {
                ModelState.AddModelError("TypeId", $"You're trying to assign product to not existing type [{productResource.TypeId}]");
            }

            var unit = unitRepository.Get(productResource.UnitId);
            if (unit == null)
            {
                ModelState.AddModelError("UnitId", $"You're trying to assign product to not existing unit [{productResource.UnitId}]");
            }

            foreach (var categoryId in productResource.Categories)
            {
                if (categoryRepository.Get(categoryId) == null)
                {
                    ModelState.AddModelError("CategoryId", $"You're trying to assign product to not existing category [{categoryId}]");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = mapper.Map<ProductResource, Product>(productResource);

            productRepository.Add(product);
            unitOfWork.Complete();

            var result = mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductResource productResource)
        {
            var type = typeRepository.Get(productResource.TypeId);
            if (type == null)
            {
                ModelState.AddModelError("TypeId", "You're trying to assign product to not existing type");
            }

            var unit = unitRepository.Get(productResource.UnitId);
            if (unit == null)
            {
                ModelState.AddModelError("UnitId", "You're trying to assign product to not existing unit");
            }

            foreach (var categoryId in productResource.Categories)
            {
                if (categoryRepository.Get(categoryId) == null)
                {
                    ModelState.AddModelError("CategoryId", "You're trying to assign product to not existing category");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = productRepository.Get(id);
            mapper.Map(productResource, product);
            unitOfWork.Complete();
            var result = mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = productRepository.Get(id, LoadLevel.Basic);
            if (product == null)
            {
                return NotFound();
            }

            productRepository.Remove(product);
            unitOfWork.Complete();

            return Ok(id);
        }
    }
}