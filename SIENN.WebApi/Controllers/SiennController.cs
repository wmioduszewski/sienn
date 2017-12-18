namespace SIENN.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class SiennController : Controller
    {
        [HttpGet]
        public string Get() => "SIENN Poland";
    }
}