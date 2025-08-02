using Eduegate.PublicAPI.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eduegate.Public.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ApiControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context) : base(context)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to eduegate public API");
        }
    }
}
