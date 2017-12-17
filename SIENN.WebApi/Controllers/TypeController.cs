namespace SIENN.WebApi.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DbAccess;
    using DbAccess.Persistance;
    using DbAccess.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Services.Model;
    using Services.Resources;

    [Route("api/types")]
    public class TypeController : Controller
    {
        private readonly IGenericRepository<Type> typeRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TypeController(IGenericRepository<Type> typeRepository, IUnitOfWork uow, IMapper mapper)
        {
            this.typeRepository = typeRepository;
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<TypeResource> GetTypes()
        {
            var types = typeRepository.GetAll().ToList();
            return mapper.Map<List<Type>, List<TypeResource>>(types);
        }

        [HttpGet("{id}")]
        public IActionResult GetType(int id)
        {
            var type = typeRepository.Get(id);
            if (type == null)
            {
                return NotFound();
            }
            var typeResource = mapper.Map<Type, TypeResource>(type);
            return Ok(typeResource);
        }

        [HttpPost]
        public IActionResult CreateType([FromBody] TypeResource typeResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var type = mapper.Map<TypeResource, Type>(typeResource);

            typeRepository.Add(type);
            uow.Complete();

            var result = mapper.Map<Type, TypeResource>(type);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateType(int id, [FromBody] TypeResource typeResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var type = typeRepository.Get(id);
            mapper.Map(typeResource, type);

            uow.Complete();
            var result = mapper.Map<Type, TypeResource>(type);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteType(int id)
        {
            var type = typeRepository.Get(id);
            if (type == null)
            {
                return NotFound();
            }

            typeRepository.Remove(type);
            uow.Complete();

            return Ok(id);
        }
    }
}