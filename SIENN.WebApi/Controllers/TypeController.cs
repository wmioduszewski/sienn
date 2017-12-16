namespace SIENN.WebApi.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DbAccess.Persistance;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Services.Model;
    using Services.Resources;

    [Route("api/types")]
    public class TypeController : Controller
    {
        private readonly SiennDbContext context;
        private readonly IMapper mapper;

        public TypeController(SiennDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<TypeResource> GetTypes()
        {
            var types = context.Type.ToList();
            return mapper.Map<List<Type>, List<TypeResource>>(types);
        }

        [HttpPost]
        public IActionResult CreateType([FromBody] TypeResource typeResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var type = mapper.Map<TypeResource, Type>(typeResource);

            context.Type.Add(type);
            context.SaveChanges();

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

            var type = context.Type.Find(id);
            mapper.Map(typeResource, type);

            context.SaveChanges();
            var result = mapper.Map<Type, TypeResource>(type);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteType(int id)
        {
            var type = context.Type.Find(id);
            if (type == null)
            {
                return NotFound();
            }

            context.Remove(type);
            context.SaveChanges();

            return Ok(id);
        }
    }
}