using Eduegate.Application.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class CacheController : BaseController
    {
        [HttpGet]
        public IActionResult ClearCaches()
        {
            Eduegate.Framework.CacheManager.MemCacheManager<string>.ClearAll();
            return Ok(new { Message = "Cache cleared" });
        }

        //[HttpGet]
        //[Route("CacheCounts")]
        //public IActionResult CacheCounts()
        //{
        //    return Ok(Framework.CacheManager.MemCacheManager<string>.Count());
        //}

        //[HttpGet]
        //[Route("GetCache")]
        //public IActionResult GetCache(string key)
        //{
        //    return Ok(Framework.CacheManager.MemCacheManager<object>.GetAsString(key));
        //}

        //[HttpGet]
        //[Route("GetAllCaches")]
        //public IActionResult GetAllCaches()
        //{
        //    var caches = Framework.CacheManager.MemCacheManager<object>.GetAll();
        //    var sb = new StringBuilder();
        //    sb.Append("<html><body>");
        //    sb.Append("<table width=100%><tr>");

        //    foreach (var cache in caches)
        //    {
        //        sb.Append("<td>");
        //        sb.Append(cache.Key);
        //        sb.Append("</td>");
        //        sb.Append("<td>");
        //        sb.Append(cache.Value);
        //        sb.Append("</td>");
        //    }

        //    sb.Append("</tr></table>");
        //    sb.Append("</body></html>");

        //    return new ContentResult
        //    {
        //        ContentType = "text/html",
        //        StatusCode = (int)HttpStatusCode.OK,
        //        Content = sb.ToString()
        //    };
        //}

    }
}