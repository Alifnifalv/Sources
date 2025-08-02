using Eduegate.Application.Mvc;
using Eduegate.Domain.Frameworks;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        public async Task<IActionResult> Recent(int count)
        {
            try
            {
                return Content(await new ErrorBL(CallContext).Recent(count), "text/html");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve or execute the error logs. Exception: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Recent(int count, string type = null)
        {
            try
            {
                if (type == "json")
                {
                    return Ok(await new ErrorBL(CallContext).Recent(count, type, null));
                }
                else
                {
                    return Content(await new ErrorBL(CallContext).Recent(count, type, null), "text/html");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve or execute the error logs. Exception: {ex.Message}");
            }
        }
    }
}
