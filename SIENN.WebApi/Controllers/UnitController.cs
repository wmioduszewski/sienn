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

    [Route("api/units")]
    public class UnitController : Controller
    {
        private readonly IGenericRepository<Unit> unitRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UnitController(IGenericRepository<Unit> unitRepository, IUnitOfWork uow, IMapper mapper)
        {
            this.unitRepository = unitRepository;
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<UnitResource> GetTypes()
        {
            var units = unitRepository.GetAll().ToList();
            return mapper.Map<List<Unit>, List<UnitResource>>(units);
        }

        [HttpGet("{id}")]
        public IActionResult GetUnit(int id)
        {
            var unit = unitRepository.Get(id);
            if (unit == null)
            {
                return NotFound();
            }
            var unitResource = mapper.Map<Unit, UnitResource>(unit);
            return Ok(unitResource);
        }

        [HttpPost]
        public IActionResult CreateUnit([FromBody] UnitResource unitResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unit = mapper.Map<UnitResource, Unit>(unitResource);

            unitRepository.Add(unit);
            uow.Complete();

            var result = mapper.Map<Unit, UnitResource>(unit);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUnit(int id, [FromBody] UnitResource unitResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unit = unitRepository.Get(id);
            mapper.Map(unitResource, unit);

            uow.Complete();
            var result = mapper.Map<Unit, UnitResource>(unit);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUnit(int id)
        {
            var unit = unitRepository.Get(id);
            if (unit == null)
            {
                return NotFound();
            }

            unitRepository.Remove(unit);
            uow.Complete();

            return Ok(id);
        }
    }
}