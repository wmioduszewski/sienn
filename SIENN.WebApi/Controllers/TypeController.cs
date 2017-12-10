namespace SIENN.WebApi.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using DbAccess.Persistance;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Services.Model;

    public class TypeController : Controller
    {
        private readonly SiennDbContext context;

        public TypeController(SiennDbContext context)
        {
            this.context = context;
        }

        [HttpGet("api/types")]
        public IEnumerable<Type> GetTypes()
        {
            return context.Type.ToList();
        }
    }
}