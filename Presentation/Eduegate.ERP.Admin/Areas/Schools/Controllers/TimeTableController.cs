using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    public class TimeTableController : BaseSearchController
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

        public JsonResult GetSubjectByClassID(int classId)
        {
            var classSub = ClientFactory.SchoolServiceClient(CallContext).GetSubjectByClass(classId);
            return Json(classSub, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetTimeTableByClassID(int classId, int tableMasterId)
        {
            var timeTableList = ClientFactory.SchoolServiceClient(CallContext).GetTimeTableByClassID(classId, tableMasterId);
            return Json(timeTableList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTimeTableByDate(int classId, int tableMasterId, string timeTableDate)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            DateTime date = string.IsNullOrEmpty(timeTableDate) ? (DateTime)System.DateTime.Now : DateTime.ParseExact(timeTableDate, dateFormat, CultureInfo.InvariantCulture);

            var timeTableList = ClientFactory.SchoolServiceClient(CallContext).GetTimeTableByDate(classId, tableMasterId, date);
            return Json(timeTableList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTimeTable(TimeTableAllocationViewModel timeTableInfo)
        {
            string timeTableAllocation = "0";

            if (timeTableInfo.ClassId.HasValue && timeTableInfo.ClassId.Value != 0 && timeTableInfo.SectionID.HasValue)
            {
                List<long> staffList = (from t in timeTableInfo.StaffList
                                        select long.Parse(t.Key)).ToList();

                timeTableAllocation = ClientFactory.SchoolServiceClient(CallContext).SaveTimeTable(new Services.Contracts.School.Academics.TimeTableAllocationDTO()
                {
                    ClassId = timeTableInfo.ClassId,
                    SectionID = timeTableInfo.SectionID,
                    WeekDayID = timeTableInfo.WeekDayID,
                    ClassTimingID = timeTableInfo.ClassTimingID,
                    StaffIDList = staffList,
                    SubjectID = timeTableInfo.SubjectID,
                    TimeTableID = timeTableInfo.TimeTableID,
                    TimeTableAllocationIID = timeTableInfo.TimeTableAllocationIID
                });
            }

            return Json(timeTableAllocation, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTimeTableLog(TimeTableLogViewModel timeTableInfo)
        {
            string timeTableLogID = "0";
            if (timeTableInfo.ClassId.HasValue && timeTableInfo.ClassId.Value != 0 && timeTableInfo.SectionID.HasValue)
            {
                var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
                DateTime allocatedDate = DateTime.ParseExact(Convert.ToString(timeTableInfo.AllocatedDateString), dateFormat, CultureInfo.InvariantCulture);

                timeTableLogID = ClientFactory.SchoolServiceClient(CallContext).SaveTimeTableLog(new Services.Contracts.School.Academics.TimeTableLogDTO()
                {
                    ClassId = timeTableInfo.ClassId,
                    SectionID = timeTableInfo.SectionID,
                    WeekDayID = timeTableInfo.WeekDayID,
                    ClassTimingID = timeTableInfo.ClassTimingID,
                    StaffID = timeTableInfo.StaffID,
                    SubjectID = timeTableInfo.SubjectID,
                    TimeTableID = timeTableInfo.TimeTableID,
                    TimeTableLogID = timeTableInfo.TimeTableLogID,
                    TimeTableAllocationID = timeTableInfo.TimeTableAllocationID,
                    AllocatedDate = allocatedDate
                });
            }

            return Json(timeTableLogID, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteTimeTableEntry(string timeTableAllocationId)
        {
            ClientFactory.SchoolServiceClient(CallContext).DeleteTimeTableEntry(timeTableAllocationId);
            return Json(timeTableAllocationId, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteDailyTimeTableEntry(long timeTableLogID)
        {
            ClientFactory.SchoolServiceClient(CallContext).DeleteDailyTimeTableEntry(timeTableLogID);
            return Json(timeTableLogID, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GenerateTimeTable(TimeTableAllocationViewModel timeTableInfo)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            var timeTableLog = new TimeTableAllocationDTO()
            {
                TimeTableMasterID = timeTableInfo.TimeTableMasterID,
                ClassId = timeTableInfo.ClassId,
                IsGenerate = timeTableInfo.IsGenerate,
                AllocatedDate = string.IsNullOrEmpty(timeTableInfo.AllocatedDateString) ? (DateTime?)null : DateTime.ParseExact(timeTableInfo.AllocatedDateString, dateFormat, CultureInfo.InvariantCulture),
            };

            var timeTableGenerate = ClientFactory.SchoolServiceClient(CallContext).GenerateTimeTable(timeTableLog);

            return Json(timeTableGenerate, JsonRequestBehavior.AllowGet);
        }

    }
}