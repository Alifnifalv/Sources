using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    public class SubjectTeacherMapController : BaseSearchController
    {
        // GET: Schools/TimeTable
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TimeTableBuilder()
        {
            return View();
        }

     

        public JsonResult GetTeacherBySubject(int classId, int SectionId, int subjectId)
        {
            var timeTableList = ClientFactory.SchoolServiceClient(CallContext).GetTeacherBySubject(classId, SectionId, subjectId);
            var list = (from t in timeTableList
                        select new KeyValueViewModel() { Key = t.EmployeeID.ToString(), Value = t.TeacherName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
}