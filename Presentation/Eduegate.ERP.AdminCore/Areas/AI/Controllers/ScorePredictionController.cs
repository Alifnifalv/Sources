using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Domain.AI;

namespace Eduegate.ERP.Admin.Areas.AI.Controllers
{
    [Area("AI")]
    public class ScorePredictionController : BaseSearchController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ScoreBoard()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetScorePrediction(long studentID)
        {
            var result = new ScorePredictionBL(CallContext).GetScorePrediction(studentID);

            if (result == null)
            {
                return Json(new { success = false, message = result });
            }
            else
            {
                return Json(new { success = true, message = result });
            }
        }

    }
}