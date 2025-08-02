using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.School.Academics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Extensions;

namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    [Area("Schools")]
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
            return Json(classSub);
        }


        public JsonResult GetTimeTableByClassID(int classId, int tableMasterId)
        {
            var timeTableList = ClientFactory.SchoolServiceClient(CallContext).GetTimeTableByClassID(classId, tableMasterId);
            return Json(timeTableList);
        }
        public JsonResult GetTimeTableByDate(int classId, int tableMasterId, string timeTableDate)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            DateTime date = string.IsNullOrEmpty(timeTableDate) ? (DateTime)System.DateTime.Now : DateTime.ParseExact(timeTableDate, dateFormat, CultureInfo.InvariantCulture);

            var timeTableList = ClientFactory.SchoolServiceClient(CallContext).GetTimeTableByDate(classId, tableMasterId, date);
            return Json(timeTableList);
        }

        [HttpPost]
        public ActionResult SaveTimeTable(TimeTableAllocationViewModel timeTableInfo)
        {
            string timeTableAllocation = "0";

            if (timeTableInfo.ClassId.HasValue && timeTableInfo.ClassId.Value != 0 && timeTableInfo.SectionID.HasValue)
            {
                //List<long> staffList = (from t in timeTableInfo.StaffList
                //                        select long.Parse(t.Key)).ToList();

                List<long> staffList = new List<long>();
                staffList.AddRange(timeTableInfo.StaffID);

                timeTableAllocation = ClientFactory.SchoolServiceClient(CallContext).SaveTimeTable(new Services.Contracts.School.Academics.TimeTableAllocationDTO()
                {
                    ClassId = timeTableInfo.ClassId,
                    SectionID = timeTableInfo.SectionID,
                    WeekDayID = timeTableInfo.WeekDayID,
                    ClassTimingID = timeTableInfo.ClassTimingID,
                    StaffIDList = staffList,
                    SubjectID = timeTableInfo.SubjectID,
                    TimeTableID = timeTableInfo.TimeTableID,
                    TimeTableAllocationIID = timeTableInfo.TimeTableAllocationIID,
                    IsEnteredManually = true,
                });
            }

            return Json(timeTableAllocation);
        }

        [HttpPost]
        public ActionResult SaveTimeTableLog(TimeTableLogViewModel timeTableInfo)
        {
            string timeTableLogID = "0";
            if (timeTableInfo.ClassId.HasValue && timeTableInfo.ClassId.Value != 0 && timeTableInfo.SectionID.HasValue)
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
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
                    AllocatedDate = allocatedDate,
                    IsEnteredManually = true,
                });
            }

            return Json(timeTableLogID);
        }
        [HttpPost]
        public ActionResult DeleteTimeTableEntry(long timeTableAllocationID)
        {
            ClientFactory.SchoolServiceClient(CallContext).DeleteTimeTableEntry(timeTableAllocationID);
            return Json(timeTableAllocationID);
        }
        [HttpPost]
        public ActionResult DeleteDailyTimeTableEntry(long timeTableLogID)
        {
            ClientFactory.SchoolServiceClient(CallContext).DeleteDailyTimeTableEntry(timeTableLogID);
            return Json(timeTableLogID);
        }

        [HttpPost]
        public ActionResult GenerateTimeTable(TimeTableAllocationViewModel timeTableInfo)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var timeTableLog = new TimeTableAllocationDTO()
            {
                TimeTableMasterID = timeTableInfo.TimeTableMasterID,
                ClassId = timeTableInfo.ClassId,
                IsGenerate = timeTableInfo.IsGenerate,
                AllocatedDate = string.IsNullOrEmpty(timeTableInfo.AllocatedDateString) ? (DateTime?)null : DateTime.ParseExact(timeTableInfo.AllocatedDateString, dateFormat, CultureInfo.InvariantCulture),
                WeekDayID = timeTableInfo.WeekDayID.IsNull() ? int.Parse(ClientFactory.SchoolServiceClient(CallContext).GetWeekDayByDate(timeTableInfo.AllocatedDateString)[0].Key) : timeTableInfo.WeekDayID,
            };

            var timeTableGenerate = ClientFactory.SchoolServiceClient(CallContext).GenerateTimeTable(timeTableLog);

            return Json(timeTableGenerate);
        }

        public List<DaysDTO> GetWeekDays()
        {
            return ClientFactory.SchoolServiceClient(CallContext).GetWeekDays();
        }

        public List<TimeTableAllocInfoHeaderDTO> GetSmartTimeTableByDate(int tableMasterId, string timeTableDate, int classID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            DateTime date = string.IsNullOrEmpty(timeTableDate) ? (DateTime)System.DateTime.Now : DateTime.ParseExact(timeTableDate, dateFormat, CultureInfo.InvariantCulture);

            return ClientFactory.SchoolServiceClient(CallContext).GetSmartTimeTableByDate(tableMasterId, date, classID);
        }

        public List<TimeTableListDTO> GetTeacherSummary(int tableMasterID)
        {
            return ClientFactory.SchoolServiceClient(CallContext).GetTeacherSummary(tableMasterID);
        }

        public List<TimeTableListDTO> GetClassSummary(int tableMasterID)
        {
            return ClientFactory.SchoolServiceClient(CallContext).GetClassSummary(tableMasterID);
        }

        public List<TimeTableAllocationDTO> GetClassSectionTimeTableSummary(int tableMasterID)
        {
            return ClientFactory.SchoolServiceClient(CallContext).GetClassSectionTimeTableSummary(tableMasterID);
        }

        public List<TimeTableListDTO> GetTeacherSummaryByTeacherID(long employeeID, int tableMasterID)
        {
            return ClientFactory.SchoolServiceClient(CallContext).GetTeacherSummaryByTeacherID(employeeID, tableMasterID);
        }

        public List<TimeTableListDTO> GetClassSummaryDetails(long classID, long sectionID, int tableMasterID)
        {
            return ClientFactory.SchoolServiceClient(CallContext).GetClassSummaryDetails(classID, sectionID, tableMasterID);
        }

        public string GenerateSmartTimeTable(int tableMasterId, string timeTableDate)
        {
            return ClientFactory.SchoolServiceClient(CallContext).GenerateSmartTimeTable(tableMasterId, timeTableDate);
        }

        public OperationResultDTO ReAssignTimeTable(int tableMasterId, string timeTableDate, int classID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            DateTime date = string.IsNullOrEmpty(timeTableDate) ? (DateTime)System.DateTime.Now : DateTime.ParseExact(timeTableDate, dateFormat, CultureInfo.InvariantCulture);

            return ClientFactory.SchoolServiceClient(CallContext).ReAssignTimeTable(tableMasterId, date, classID);
        }

        public JsonResult GetSubjectDetailsByClassID(int classId)
        {
            var classSub = ClientFactory.SchoolServiceClient(CallContext).GetSubjectDetailsByClassID(classId);
            return Json(classSub);
        }

        public JsonResult GetWeekDayByDate(string date)
        {
            var days = ClientFactory.SchoolServiceClient(CallContext).GetWeekDayByDate(date);

            return Json(days);
        }

        public JsonResult GetSetupScreens()
        {
            var days = ClientFactory.SchoolServiceClient(CallContext).GetSetupScreens();

            return Json(days);
        }
    }
}