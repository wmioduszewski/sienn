namespace SIENN.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DbAccess.Persistance;
    using Microsoft.AspNetCore.Mvc;
    using Services.Model;
    using Services.Resources;

    [Route("api/units")]
    public class UnitController : Controller
    {
        private readonly SiennDbContext context;
        private readonly IMapper mapper;

        public UnitController(SiennDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<UnitResource> GetTypes()
        {
            var units = context.Unit.ToList();
            return mapper.Map<List<Unit>, List<UnitResource>>(units);
        }

        [HttpPost]
        public IActionResult CreateUnit([FromBody] UnitResource unitResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unit = mapper.Map<UnitResource, Unit>(unitResource);

            context.Unit.Add(unit);
            context.SaveChanges();

            var result = mapper.Map<Unit, UnitResource>(unit);

            return Ok(result);
        }
    }
}